﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMessagesPanel : MonoBehaviour
{
	private void Start()
	{
		StartCoroutine(TestCoroutine());
	}

	private IEnumerator TestCoroutine()
	{
		yield return new WaitForSeconds(2);
		MessagePanel.AddMessageInQueue("Привет");
		MessagePanel.AddMessageInQueue("Как дела?)");
		MessagePanel.AddMessageInQueue("так, мне пока не написали про город ничего и про здания. какое окружение у города должно быть? есть ли там на заднем плане здания какие-то? или там просто деревья какие-то? что за предметы внутри зданий: всякие кактусы, батарейки и шо-то еще");
	}
}