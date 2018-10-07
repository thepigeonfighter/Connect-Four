using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace ConnectFour
{
    public class ScrollBarController : MonoBehaviour
    {
        public bool hasBeenClicked;
        private Scrollbar scrollbar;
        // Use this for initialization
        void Start()
        {
            scrollbar = GetComponent<Scrollbar>();
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetMouseButtonDown(0)|| Input.GetAxis("Mouse ScrollWheel")!= 0)
            {
                UnlockScrollBar();
            }
            if (!hasBeenClicked)
            {
                scrollbar.value = 0;
            }
        }
        private void UnlockScrollBar()
        {
            hasBeenClicked = true;
        }
        public void Reset()
        {
            hasBeenClicked = false;
        }
    }
}