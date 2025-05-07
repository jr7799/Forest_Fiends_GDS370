using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

namespace Art_Controller
{
    /*General Code Could Be Used In Your Projects*/

    //State of Character
    public enum State { Idle, Walk, Attack, Skill, SecondSkill, Die };
    //State of Character Skill
    public enum ActionType { Attack, Skill, SecondSkill}

    //Character Information
    [System.Serializable]
    public class CharacterInfo
    {
        //The Prefab of Character
        public GameObject prefab;
        //Certain Character will spawn prefab in action, here is the list of the skills (including prefab, detail info of each skill)
        public List<ActionInfo> actions;
        //The Position Where the action prefab will be spawn from the position of the character
        public Vector3 characterOffset;
        //The Sfx info of each action of character
        public List<ActionSfx> actionSfx;
    }
    [System.Serializable]

    //Action Information for Certain Character that spawn prefab in action
    public class ActionInfo
    {
        //The type of action
        public ActionType type;
        //The prefab to be spawned in the action
        public GameObject prefab;
        //Delay Time Before the prefab spawn
        public float delay;
        //The flying speed of prefab before Hit, set to zero if the prefab have no flying time (Direct Hit)
        public float flyingSpeed;
    }

    [System.Serializable]
    public class ActionSfx
    {
        //The type of State
        public State state;
        //Times of Sfx played According to Length of List, Delay time is before each Sfx played
        public List<float> delay;
    }
    /*End of General Code*/

    /*Demo Code*/
    public class PrefabSpawner : MonoBehaviour
    {

        public List<CharacterInfo> characterInfo;
        public float prefabScale;
        private GameObject currentCharacter = null;
        private int currentIndex = -1;
        public GameObject spawnPoint;
        public GameObject stopPoint;
        public State state = State.Idle;
        public List<Sprite> buttonSprites;
        public Image buttonImage;
        public Animator characterAnimator = null;
        public GameObject summonPrefab;
        public Transform monsterTransform;
        public Vector3 monsterOffset;

        //After Spawn Button is Clicked
        public void SpawnCharacterButtonClicked(int index)
        {
            // Destroy existing Characters in Scene
            if (currentCharacter != null) RemoveCharacter();
            currentIndex = index;
            // Spawn new prefab according to index
            StartCoroutine(SummonCircle());
        }

        public void SpawnPrefabCharacter()
        {
            currentCharacter = Instantiate(characterInfo[currentIndex].prefab, spawnPoint.transform.position, Quaternion.identity);
            characterAnimator = currentCharacter.GetComponent<Animator>();
            state = State.Idle;
            characterAnimator.Play("idle");
            StartCoroutine(Fade(true));
        }

        public void RemoveCharacter()
        {
            // Destroy existing Characters in Scene
            Destroy(currentCharacter);
            currentCharacter = null;
            characterAnimator = null;
            state = State.Idle;
        }

        private IEnumerator SpawnSkill(ActionInfo skillInfo)
        {
            yield return new WaitForSeconds(skillInfo.delay);
            GameObject skillPrefab = Instantiate(skillInfo.prefab, currentCharacter.transform.position + characterInfo[currentIndex].characterOffset, Quaternion.identity);
            Animator skillAnimator = skillPrefab.GetComponent<Animator>();
            //Skill Flying
            if (skillInfo.flyingSpeed > 0)
            {
                skillAnimator.Play("flying");
                while (Vector3.Distance(skillPrefab.transform.position, monsterTransform.position + monsterOffset) > Mathf.Epsilon)
                {
                    skillPrefab.transform.position = Vector3.MoveTowards(skillPrefab.transform.position, monsterTransform.position+ monsterOffset, Time.deltaTime * skillInfo.flyingSpeed);
                    yield return null;
                    
                }
            }
            //Skill Hit
            skillPrefab.transform.position = monsterTransform.position + monsterOffset;
            skillAnimator.Play("hit");
            //Skill Hit SFX
            skillPrefab.GetComponent<AudioSource>().PlayOneShot(skillPrefab.GetComponent<AudioSource>().clip);
            yield return new WaitUntil(() => skillAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);
            //Fade
            var spriteRenderer = skillPrefab.GetComponent<SpriteRenderer>();
            float transparency = spriteRenderer.color.a;
            Animator animator = skillPrefab.GetComponent<Animator>();
            float duration = animator.GetCurrentAnimatorStateInfo(0).length;
            while (transparency > 0)
            {
                transparency -= 0.1f;
                spriteRenderer.color = new Color(1, 1, 1, transparency);
                yield return new WaitForSeconds(duration * 0.2f);
            }
            Destroy(skillPrefab);
        }

        //After Action Button is Clicked
        public void ActionChange()
        {
            if (currentCharacter == null) return;
            AnimationClip[] clips = characterAnimator.runtimeAnimatorController.animationClips;
            if (state == State.Idle)
            {
                characterAnimator.Play("walk");
                state = State.Walk;
            }
            else if (state == State.Walk && clips.Any(x => x.name == "attack"))
            {
                //Attack Animation
                characterAnimator.Play("attack");
                state = State.Attack;
                //Attack Sfx
                StartCoroutine(ActionSFX());
                //Attack Prefab
                if (characterInfo[currentIndex].actions.Count > 0 && characterInfo[currentIndex].actions.Any(x => x.type == ActionType.Attack))
                    StartCoroutine(SpawnSkill(characterInfo[currentIndex].actions.FirstOrDefault(x => x.type == ActionType.Attack)));
            }
            else if (state == State.Attack && clips.Any(x => x.name == "skill"))
            {
                //Skill Animation
                characterAnimator.Play("skill");
                state = State.Skill;
                //Skill Sfx
                StartCoroutine(ActionSFX());
                //Skill Prefab
                if (characterInfo[currentIndex].actions.Count > 0 && characterInfo[currentIndex].actions.Any(x => x.type == ActionType.Skill))
                    StartCoroutine(SpawnSkill(characterInfo[currentIndex].actions.FirstOrDefault(x => x.type == ActionType.Skill)));
            }
            else if (state == State.Skill && clips.Any(x => x.name == "secondSkill"))
            {
                //SecondSkill Animation
                characterAnimator.Play("secondSkill");
                state = State.SecondSkill;
                //SecondSkill Sfx
                StartCoroutine(ActionSFX());
                //Second Skill Prefab
                if (characterInfo[currentIndex].actions.Count > 0 && characterInfo[currentIndex].actions.Any(x => x.type == ActionType.SecondSkill))
                    StartCoroutine(SpawnSkill(characterInfo[currentIndex].actions.FirstOrDefault(x => x.type == ActionType.SecondSkill)));
            }
            else 
            {
                characterAnimator.Play("die");
                state = State.Die;
                StartCoroutine(Fade(false));
            }
        }

        private void Update()
        {
            buttonImage.sprite = buttonSprites[(int)state];
            if (characterAnimator != null) 
            {
                //Set Front or Back Sprite
                characterAnimator.SetFloat("movementY", -1);
            }
            if (state == State.Walk)
            {
                currentCharacter.transform.position = Vector3.MoveTowards(currentCharacter.transform.position, stopPoint.transform.position, Time.deltaTime*2);
                if(currentCharacter.transform.position == stopPoint.transform.position) characterAnimator.Play("idle");
            }
        }


        //Demo Effects
        public IEnumerator SummonCircle()
        {
            Vector3 position = spawnPoint.transform.position;
            GameObject prefab = Instantiate(summonPrefab, new Vector3(position.x, position.y+0.3f, position.z), Quaternion.identity);
            SpriteRenderer spriteRenderer = prefab.GetComponent<SpriteRenderer>();
            float transparency = spriteRenderer.color.a;
            Animator animator = prefab.GetComponent<Animator>();
            yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5);
            prefab.GetComponent<AudioSource>().PlayOneShot(prefab.GetComponent<AudioSource>().clip) ;
            float duration = animator.GetCurrentAnimatorStateInfo(0).length;
            SpawnPrefabCharacter();
            while (transparency > 0)
            {
                transparency -= 0.1f;
                spriteRenderer.color = new Color(1, 1, 1, transparency);
                yield return new WaitForSeconds(duration * 0.1f);
            }
            Destroy(prefab);
        }
        //Demo Effects Fade
        public IEnumerator Fade(bool fadeIn)
        {
            SpriteRenderer spriteRenderer = currentCharacter.GetComponent<SpriteRenderer>();
            spriteRenderer.color = new Color(1, 1, 1, fadeIn ? 0 : 1);
            float transparency = spriteRenderer.color.a;
            while (fadeIn ? transparency < 1 : transparency > 0)
            {
                transparency += fadeIn ? 0.1f : -0.1f;
                spriteRenderer.color = new Color(1, 1, 1, transparency);
                yield return new WaitForSeconds(0.1f);
            }
            if (!fadeIn) RemoveCharacter();
        }

        public IEnumerator ActionSFX()
        {
            ActionSfx currentActionSfx = characterInfo[currentIndex].actionSfx.FirstOrDefault(x=>x.state == state);
            AudioSource characterAudio = currentCharacter.GetComponent<AudioSource>();
            if (currentActionSfx == null) yield break;
            foreach (float second in currentActionSfx.delay)
            {
                yield return new WaitForSeconds(second);
                characterAudio.PlayOneShot(characterAudio.clip);
            }
        }
    }

}
