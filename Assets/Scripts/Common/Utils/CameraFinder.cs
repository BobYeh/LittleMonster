using UnityEngine;

namespace Common.Utils
{
    public class CameraFinder : MonoBehaviour
    {
        public Camera DisposeCamera;
        private Canvas _canvas;

        // Use this for initialization
        void Start()
        {
            this._canvas = this.GetComponent<Canvas>();

            if (Camera.main != null
                && this.DisposeCamera != null
                && !System.Object.ReferenceEquals(this.DisposeCamera, Camera.main))
            {
                this._canvas.worldCamera = Camera.main;
                Destroy(this.DisposeCamera.gameObject);
            }

            if (Camera.main != null && this._canvas.worldCamera == null)
            {
                this._canvas.worldCamera = Camera.main;
            }
        }
    }
}