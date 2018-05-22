using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IMovingPlatform<T>
{
	void EnterMovingPlatform(T parent);

	void ExitMovingPlatform(T parent);
}
