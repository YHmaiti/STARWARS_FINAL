using Jackknife;
using System.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using DigitalRuby.LightningBolt;
using Unity.VisualScripting;

namespace Gestures
{
    public class Recognizer : MonoBehaviour
    {
        //public lightning lightning_var;
        [SerializeField] AudioClip LightningSound;
        
        // TODO make the classification async
        [Tooltip("Don't touch, unless want to use a different set (requires modifying the Dictionary)")]
        public string gestureDirectoryPath;

        [Tooltip("Disable action execution (for customization mode)")]
        public bool disableActionExecution = true;

        [Tooltip("Where do we print the classification result")]
        public TMP_Text debugText;

        [Tooltip("Use to pass direction to the direction-based selection technique")]
        public bool useAsDirection;

        private Jackknife.Jackknife jackknife;

        private readonly Dictionary<string, int> GestureNameToGestureId = new Dictionary<string, int>()
        {
            // Must use dashes instead of spaces or underscores (because of file name parsing method)
            {"sphere", 1 },
            {"cylinder", 2 },
            {"cube", 3 },
            {"star", 4 },
            {"pyramid", 5 },
            {"infinity", 6 }
        };

        private Dictionary<int, string> GestureIdToGestureName;

        private void Start()
        {
            if (Application.isEditor)
            {
                gestureDirectoryPath = Application.dataPath + "/Dataset/";
            }
            else
            {
                gestureDirectoryPath = Application.persistentDataPath + "/Dataset/";
            }

            print("Gesture Dir:" + gestureDirectoryPath);

            CreateGestureIdToNameMapping();
            CreateGestureRecognizer();
            SummarizeTraining();
            TestClassification();
        }

        // Gets automatically called when the person releases the gesture performance button
        public void ProcessGestureBuffer(List<Vector> gestureBufferTrajectory)
        {
            if (useAsDirection)
            {
                RecognitionResult r = RecognizerUtils.constructRecognitionResult(
                    -1,
                    "unknown",
                    gestureBufferTrajectory[0],
                    gestureBufferTrajectory[gestureBufferTrajectory.Count - 1]);

                SelectionEvents.DirectionSelection.Invoke(r);
            }
            else
            {
                // If either not enough points, or path traveled is less than half a me
                if (gestureBufferTrajectory.Count < 12 || Jackknife.Mathematics.PathLength(gestureBufferTrajectory) < .3)
                {
                    return;
                }

                int classifiedGestureId = ClassifyTrajectory(gestureBufferTrajectory);
                string classifiedGestureName = GetGestureNameFromId(classifiedGestureId);

                Debug.Log($"Classified as {classifiedGestureName}");

                if (classifiedGestureId == -1)
                {
                    return;
                }

                //SelectionEvents.FilterSelection.Invoke(classifiedGestureName);

                Mathematics.BoundingBox(gestureBufferTrajectory, out Vector minPoint, out Vector maxPoint);

                RecognitionResult r = RecognizerUtils.constructRecognitionResult(
                    classifiedGestureId,
                    classifiedGestureName,
                    minPoint,
                    maxPoint);

                SelectionEvents.FilterSelection.Invoke(r);

                CallGestureAction(classifiedGestureId);
            }
        }
        Transform originalParent;
        private void CallGestureAction(int gestureId)
        {

            foreach (var gesture in GestureIdToGestureName)
            {
                Debug.Log($"Gesture: {gesture.Key} - {gesture.Value}");
            }
            if (disableActionExecution)
            {
                return;
            }

            switch (gestureId)
            {
                case 1: // sphere


                    break;

                case 2: // cylindre
                    break;

                case 3: // cube
                    // FIND A GAMEOBJKECT CALLED LightSaber2 AND MAKE IT A SHILD OF THE LEFT HAND
                    GameObject LightSaber2 = GameObject.Find("LightSaber2");
                    GameObject LeftHand = GameObject.Find("LeftHand Controller");
                    LightSaber2.transform.SetParent(LeftHand.transform);
                    LightSaber2.transform.localPosition = new Vector3(0, 0, 0);
                    LightSaber2.transform.localRotation = Quaternion.Euler(0, 0, 0);
                    break;  

                case 4: // star
                    // Two hands forward
                    //Events.spawn_lightning.Invoke();
                    // Afind lightning game object
                    // lightning lightning_var = GameObject.Find("Lightning").GetComponent<lightning>();
                    // print($"HELLO {lightning_var.name}");
                    // activate lightning
                    //if (lightning_var.recharge == true)
                    //{
                    //lightning_var.activate = true;
                    /*  lightning_var.gameObject.SetActive(true);
                      StartCoroutine(Deactivate(lightning_var.gameObject));
                  }*/
                    GameObject lightningObject = GameObject.Find("LightningObject");
                    lightningObject.GetComponent<LightningBoltScript>().Trigger();
                    lightningObject.GetComponent<Collider>().enabled  = true;
                    lightningObject.GetComponent<AudioSource>().PlayOneShot(LightningSound);
                    if (lightningObject.GetComponent<LightningBoltScript>().ManualMode)
                    {
                        //stop playing audio
                       // lightningObject.GetComponent<Collider>().enabled = false;
                        StartCoroutine(Deactivate());
                    }

                    Debug.Log("Lightning Bolt normal Triggered" + GestureIdToGestureName[gestureId]); // star
                    break;

                case 5: // pyramid
                    // heal - two hands up and forward, or cross
                    //Events.heal_event.Invoke();
                    GameObject Player = GameObject.Find("Player_XR Origin");
                    Player.GetComponent<playerHP>().selfHeal();
                    Debug.Log("Heal Triggered" + GestureIdToGestureName[gestureId]); // pyramid
                    break;

                case 6: // infinity
                    GameObject wave = GameObject.Find("wave");

                    if (wave == null)
                    {
                        Debug.LogError("Wave object not found!");
                        return;
                    }

                    Debug.Log("Wave unparented");

                    // Activate the wave
                    wave.SetActive(true);

                    // Enable its collider
                    Collider waveCollider = wave.GetComponent<Collider>();
                    if (waveCollider == null)
                    {
                        Debug.LogError("Wave object does not have a Collider component!");
                    }
                    else
                    {
                        waveCollider.enabled = true;
                        wave.GetComponent<BoxCollider>().enabled = true;
                        // make it a trigger
                        waveCollider.isTrigger = true;
                    }

                    // Apply velocity
                    Rigidbody waveRb = wave.GetComponent<Rigidbody>();

                    GameObject HMD = GameObject.Find("Main Camera");
                    if (waveRb == null)
                    {
                        Debug.LogError("Wave object does not have a Rigidbody component!");
                    }
                    else
                    {
                        //waveRb.velocity = transform.forward * 20f;

                        // send the wave from its current roation to the forward direction of the HMD
                        waveRb.velocity = HMD.transform.forward * 20f;

                        // make the wave a trigger
                        waveCollider.isTrigger = true;
                    }

                    // Debug gesture info safely
                    if (GestureIdToGestureName.TryGetValue(gestureId, out string gestureName))
                    {
                        Debug.Log("Wave going forward " + gestureName);
                    }
                    else
                    {
                        Debug.LogWarning("Gesture ID not found in dictionary!");
                    }

                    // fix the toration of the wave to be where the HMD is pointing after getting the HMD gameobject
                    wave.transform.rotation = HMD.transform.rotation;

                    // Destroy the wave after it has moved 30 meters forward
                    wave.GetComponent<MonoBehaviour>().Invoke("DestroyWave", 6f);

                    break;
                default:
                    throw new System.Exception("Call Gesture Action went wrong. Possible missclasification");
            }
        }

        private IEnumerator GrowMoveAndResetWave(GameObject wave, Transform originalParent)
        {
            Vector3 initialScale = Vector3.zero;
            Vector3 targetScale = new Vector3(30f, 30f, 30f); // Wave grows up to 30 meters
            Vector3 moveDirection = Vector3.forward; // Moves forward in world space

            float duration = 2.0f; // Time in seconds for full wave effect
            float elapsedTime = 0f;

            while (elapsedTime < duration)
            {
                float t = elapsedTime / duration;
                wave.transform.localScale = Vector3.Lerp(initialScale, targetScale, t);
                wave.transform.position += moveDirection * Time.deltaTime * 15f;

                elapsedTime += Time.deltaTime;
            }

            Debug.Log("Wave reached max size and distance");

            // Wait briefly before resetting
            yield return new WaitForSeconds(0.5f);

            // Deactivate the wave
            wave.SetActive(false);

            // Reset wave scale and position
            wave.transform.localScale = initialScale;
            wave.transform.position = new Vector3(0, 0, 0);

            Debug.Log("Wave reset and reparented");
        }


        private IEnumerator Deactivate()
        {
            yield return new WaitForSeconds(2f);
            GameObject lightningObject = GameObject.Find("LightningObject");
            lightningObject.GetComponent<AudioSource>().Stop();
            lightningObject.GetComponent<Collider>().enabled = false;
        }

        private int ClassifyTrajectory(List<Vector> trajectory)
        {
            int ret = jackknife.Classify(trajectory);

            if (debugText != null)
            {
                debugText.text = $"Classified: {GetGestureNameFromId(ret)}";
            }

            return ret;
        }

        private void TestClassification()
        {
            double result = jackknife.TestClassificationGetAccuracy();
            Debug.Log($"Internal Classification Accuracy: {result}");
        }

        public void CreateGestureRecognizer()
        {
            JackknifeBlades blades = new JackknifeBlades();
            blades.SetIPDefaults();
            blades.ResampleCnt = 20;

            jackknife = new Jackknife.Jackknife(blades);

            foreach (Sample sample in LoadJKSamples())
                jackknife.AddTemplate(sample);
        }

        public List<Sample> LoadJKSamples()
        {
            return RecognizerUtils.LoadSamples(gestureDirectoryPath, GestureNameToGestureId);
        }

        public void SummarizeTraining()
        {
            //jackknife.Train(32, 3, 1.0);
            var summary = jackknife.Summarize(GestureIdToGestureName);
            Debug.Log(summary);
        }

        private void CreateGestureIdToNameMapping()
        {
            GestureIdToGestureName = GestureNameToGestureId.ToDictionary(x => x.Value, x => x.Key);
        }

        public int GetGestureIdFromName(string gname)
        {
            if (GestureNameToGestureId.ContainsKey(gname))
                return GestureNameToGestureId[gname];
            return -1;
        }

        public string GetGestureNameFromId(int gid)
        {
            if (GestureIdToGestureName.ContainsKey(gid))
                return GestureIdToGestureName[gid];
            return "Unknown";
        }

        public List<string> GetGestureNames()
        {
            return GestureNameToGestureId.Keys.ToList();
        }
    }
}