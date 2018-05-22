using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPushable<T>
{
	void Push(T velocity);
}
