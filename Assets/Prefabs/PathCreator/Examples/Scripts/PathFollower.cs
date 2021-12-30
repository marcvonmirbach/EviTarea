using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : NetworkBehaviour
    {
        [SerializeField] private GameObject sword;
        [SerializeField] private Text score;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 3;
        float distanceTravelled;
        private Vector3 initialPos;

        [SerializeField] GameObject pathofball_prefab;

        void Start() {

            
            if (GameObject.Find("PathOfBall(Clone)") == null)
            {
                GameObject path = (GameObject)Instantiate(pathofball_prefab, new Vector3(0, 0, 0), transform.rotation);
                NetworkServer.Spawn(path);
                ClientScene.RegisterPrefab(path);

                // Runtime references
                score = GameObject.Find("Score").GetComponent<Text>();
                pathCreator = GameObject.Find("PathOfBall(Clone)").GetComponent<PathCreator>();
                sword = GameObject.Find("Sword_Joint");

                score.text = "0";
                initialPos = this.transform.position;
                if (pathCreator != null)
                {
                    // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                    pathCreator.pathUpdated += OnPathChanged;
                }
            }
            
        }

        void Update()
        {
            if (pathCreator != null)
            {
                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
        }

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
        void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }

        private void OnTriggerEnter(Collider collider)
        {
            Debug.Log(collider.gameObject.name);
            if (collider.gameObject == sword)
            {
                this.transform.position = initialPos;
                distanceTravelled = 0;
                score.text = (int.Parse(score.text) + 1).ToString();
            }
        }
    }
}