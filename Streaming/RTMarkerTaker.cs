using UnityEngine;
using System.Collections.Generic;
//Script for marker coordinates transfer to it's parent by marker name.

namespace QualisysRealTime.Unity
{

	class RTMarkerTaker : MonoBehaviour
	{

		public string markerName = "Put QTM marker name here";
		public Vector3 PositionOffset = new Vector3(0, 0, 0);

		private List<LabeledMarker> markerData;
		private RTClient rtClient;
		private GameObject markerRoot;
		private bool streaming = false;

		// Use this for initialization
		void Start()
		{
			rtClient = RTClient.GetInstance();
			markerRoot = gameObject;
		}

		// Update is called once per frame
		void Update()
		{

			if (rtClient == null) rtClient = RTClient.GetInstance();
			if (rtClient.GetStreamingStatus() && !streaming)
			{
				streaming = true;
			}
			if (!rtClient.GetStreamingStatus() && streaming)
			{
				streaming = false;
			}

			//Seeking fo desired marker name

			markerData = rtClient.Markers;
			if (markerData == null && markerData.Count == 0)
				return;
            LabeledMarker marker = markerData.Find(x => x.Label == markerName);
            if (marker!=null)
            {
                if (marker.Position != null)
                {
                    if(!float.IsNaN(marker.Position.x) && !float.IsNaN(marker.Position.y) && !float.IsNaN(marker.Position.z))
                    {
                       transform.position = marker.Position + PositionOffset;
                    }
                }
            }
            
		}
	}
}
