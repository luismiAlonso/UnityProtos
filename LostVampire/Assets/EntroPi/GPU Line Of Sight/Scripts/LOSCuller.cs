using System.Collections.Generic;
using UnityEngine;

namespace LOS
{
    [AddComponentMenu("Line of Sight/LOS Culler")]
    public class LOSCuller : MonoBehaviour
    {
        #region Exposed Data Members

        [Tooltip("Selects which layers block raycasts used for visibility calculations")]
        [SerializeField]
        private LayerMask m_RaycastLayerMask = -1;

        #endregion Exposed Data Members

        #region Private Data Members

        private bool m_IsVisible = true;
        private float timeReduceLife;
        #endregion Private Data Members

        #region Public Properties

        public LayerMask RaycastLayerMask
        {
            get { return m_RaycastLayerMask; }
            set { m_RaycastLayerMask = value; }
        }

        public bool Visibile
        {
            get { return m_IsVisible; }
        }

        public Transform target;
        public LOSSource Mysource;
        #endregion Public Properties

        #region MonoBehaviour Functions

        private void OnEnable()
        {
            enabled &= Util.Verify(GetComponent<Renderer>() != null, "No renderer attached to this GameObject! LOS Culler component must be added to a GameObject containing a MeshRenderer or Skinned Mesh Renderer!");
        }

        private void Update()
        {
            for (int i=0;i<ManagerRenderCuller.instance.listRendersCullers.Count;i++) {
               bool check= CustomCull(target, Mysource, ManagerRenderCuller.instance.listRendersCullers[i], m_RaycastLayerMask.value);
               // Debug.Log(" check " + check);
            }
        }

        #endregion MonoBehaviour Functions

        #region Private Functions

        /// <summary>
        /// Checks to see if object is inside the view frustum of any of the LOS cameras.
        /// Ideally should be called in OnWillRenderObject, but it's to late to disable renderer..
        /// Early outs when visible to one camera.
        /// </summary>
        /* private static bool culler(GameObject _self, Bounds meshBounds, int layerMask)
         {
             // Get list of sources.
             List<LOSSource> losSources = LOSManager.Instance.LOSSources;

             for (int i = 0; i < losSources.Count; ++i)
             {
                 LOSSource losSource = losSources[i];

                 if (LOSHelper.CheckBoundsVisibility(losSource, meshBounds, layerMask) && losSources[i].transform.root.name != "player")
                 {
                     if (losSources[i].transform.parent.GetComponent<IA>() != null)
                     {
                         losSources[i].transform.parent.GetComponent<IA>().detectView(true);
                     }

                     return true;
                 }
                 else if(!LOSHelper.CheckBoundsVisibility(losSource, meshBounds, layerMask) && losSources[i].transform.root.name != "player")
                 {
                     if (losSources[i].transform.parent.GetComponent<IA>()!=null) {
                         losSources[i].transform.parent.GetComponent<IA>().detectView(false);
                     }
                 }
             }

             return false;
         }*/

        private static bool CustomCull(Transform _target, LOSSource losSource, GameObject objMesh, int layerMask)
        {
            //Debug.Log("Target: "+_target.GetComponent<SimpleIA>().typeNPC + " losSource: "+ losSource+" objMesh: "+objMesh.transform.parent.GetComponent<BodyChange>());
           /* if (objMesh.tag == "Player" && LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask)) {
                Debug.Log("veo: "+objMesh.transform.parent.gameObject.name + " " + objMesh.transform.parent.gameObject.activeSelf);
                _target.GetComponent<SimpleIA>().setTarget(objMesh.transform);
                _target.GetComponent<SimpleIA>().setDetectado(true);
                return true;
            }
            else if(objMesh.tag == "Player" && !LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask)
                &&  _target.GetComponent<SimpleIA>().getDetectado())
            {
                Debug.Log("no veo: " + objMesh.transform.parent.gameObject.name + " " + objMesh.transform.parent.gameObject.activeSelf);
                if (!_target.GetComponent<SimpleIA>().ActionRangeAlerta())
                {
                    _target.GetComponent<SimpleIA>().setDetectado(false);
                    _target.GetComponent<SimpleIA>().setState(2);

                }
                else
                {
                    // Debug.Log(" alerta in but not focus " + objMesh.transform.parent.gameObject.name);

                }
                return false;
            }*/

            
             if (objMesh.tag=="Player" && LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask) &&
                objMesh.transform.parent.gameObject.activeSelf && !_target.GetComponent<SimpleIA>().getDetectado())
             {
                 // Debug.Log("te veo "+ objMesh.transform.parent.gameObject.name+" activo "+ objMesh.transform.parent.gameObject.activeSelf);
                 _target.GetComponent<SimpleIA>().setTarget(objMesh.transform);
                 _target.GetComponent<SimpleIA>().setDetectado(true);

                 return true;
             }
             else if(objMesh.tag == "Player" && !LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask) 
                 && _target.GetComponent<SimpleIA>().getDetectado() && objMesh.transform.parent.gameObject.activeSelf)
             {
                // Debug.Log(" fuera de foco " + _target.GetComponent<SimpleIA>().ActionRangeAlerta());

                 //verifico con rango alerta
                 if (!_target.GetComponent<SimpleIA>().ActionRangeAlerta()) { 
                     _target.GetComponent<SimpleIA>().setDetectado(false);
                     _target.GetComponent<SimpleIA>().setState(2);

                 }
                 else
                 {
                    // Debug.Log(" alerta in but not focus " + objMesh.transform.parent.gameObject.name);

                 }

                 return true;
             }
             else if (objMesh.tag == "Player" && LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask) && !objMesh.transform.parent.gameObject.activeSelf)
             {
                 //Debug.Log("No te veo " + objMesh.transform.parent.gameObject.name);
                 //verifico con rango alerta

                     _target.GetComponent<SimpleIA>().setDetectado(false);
                     _target.GetComponent<SimpleIA>().setState(2);

                // return true;
             }
             else if (objMesh.tag == "NPC" && _target.GetComponent<SimpleIA>().typeNPC==SimpleIA.TypeNPC.clero && objMesh.transform.parent.GetComponent<BodyChange>().dominate && !_target.GetComponent<BodyChange>().dominate &&
                 LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask) && objMesh.transform.parent.GetComponent<SimpleIA>().typeNPC== SimpleIA.TypeNPC.normal)
             {
                 // Debug.Log("veo impostor " + _target.GetComponent<SimpleIA>().typeNPC+ "//"+objMesh.transform.parent.GetComponent<SimpleIA>().typeNPC);
                 _target.GetComponent<SimpleIA>().setTarget(objMesh.transform);
                 _target.GetComponent<SimpleIA>().setDetectado(true);

               //  return true;
             }
             else if (objMesh.tag == "NPC" && _target.GetComponent<SimpleIA>().typeNPC == SimpleIA.TypeNPC.clero && objMesh.transform.parent.GetComponent<BodyChange>().dominate && !_target.GetComponent<BodyChange>().dominate &&
                 !LOSHelper.CheckBoundsVisibility(losSource, objMesh.GetComponent<Renderer>().bounds, layerMask) && _target.GetComponent<SimpleIA>().getDetectado() )
             {
                 // Debug.Log("no veo impostor"+ objMesh.transform.parent.name+" "+ _target.name);
                 _target.GetComponent<SimpleIA>().setDetectado(false);
                 _target.GetComponent<SimpleIA>().setState(2);
                 //return true;
             }


            return false;
        }

        /*private static bool CustomCull(Transform _target, Bounds meshBounds, int layerMask)
        {
            // Get list of sources.
            List<LOSSource> losSources = LOSManager.Instance.LOSSources;

            for (int i = 0; i < losSources.Count; ++i)
            {
                LOSSource losSource = losSources[i];

                if (LOSHelper.CheckBoundsVisibility(losSource, meshBounds, layerMask))
                {
                    // LA LUZ TE HA DADO

                    if (losSource.transform.tag == "Player" && losSource.transform.parent.name != "player")
                    {

                        if (_target.transform.GetComponent<ControlInteract>() != null)
                        {
                            _target.transform.GetComponent<ControlInteract>().settingDamageLifeBySun();
                            _target.transform.GetComponent<ControlInteract>().isInShadow = false;

                        }

                    }
                    else
                    {
                        //NPC TE HA VISTO

                        if (losSource.transform.parent.GetComponent<SimpleIA>() != null && _target.transform.GetComponent<PlayerControl>().enabled)
                        {
                            //Debug.Log("Te he visto");
                            losSource.transform.parent.GetComponent<SimpleIA>().setTarget(_target);
                            losSource.transform.parent.GetComponent<SimpleIA>().setDetectado(true);
                            Manager.instance.playerControl.checkers.isDetectado = true;
                        }
                    }

                    return true;
                }
                else
                {

                    if (losSource.transform.parent.GetComponent<SimpleIA>() != null 
                        && losSource.transform.parent.GetComponent<SimpleIA>().getDetectado() )
                    {
                        //Debug.Log(" No visto en sombra "+ losSource.transform.parent.name);
                        losSource.transform.parent.GetComponent<SimpleIA>().setDetectado(false);
                        losSource.transform.parent.GetComponent<SimpleIA>().setState(2);
                    }

                }

            }// end loop


            _target.transform.GetComponent<ControlInteract>().isInShadow = true;
            _target.transform.GetComponent<ControlInteract>().settingManaGlobal();

            return false;
        }*/

        #endregion Private Functions
    }
}