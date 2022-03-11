using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

namespace MagicalFlowers.UI
{
	public class MessageLogView : MonoBehaviour
	{
		[SerializeField]
		private Text logText;
		private List<string> logList;

		[SerializeField]
		private int logDataNum = 10;

		[SerializeField] private ScrollRect scrollRect;
		private StringBuilder stringBuilder;

		// Start is called before the first frame update
		void Start()
		{
			scrollRect.verticalNormalizedPosition = 0;
			logList = new List<string>();
			stringBuilder = new StringBuilder();
		}

		public void AddLogText(string logText)
		{
			logList.Add(logText);

			if (logList.Count > logDataNum)
			{
				logList.RemoveRange(0, logList.Count - logDataNum);
			}
			ViewLogText();
		}

		public void ViewLogText()
		{
			stringBuilder.Clear();

			foreach (var log in logList)
			{
				stringBuilder.Append(Environment.NewLine + log);
			}
			logText.text = stringBuilder.ToString().TrimEnd();
			UpdateView();
		}

		private void UpdateView()
		{
			scrollRect.verticalNormalizedPosition = 0;
			logText.GetComponent<ContentSizeFitter>().SetLayoutVertical();
		}
	}
}