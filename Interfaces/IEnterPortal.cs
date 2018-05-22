using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnterPortal<T>
{
	void EnterPortal(T newDirection);
}

