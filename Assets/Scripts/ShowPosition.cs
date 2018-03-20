using UnityEngine;
using System.Collections;
public class ShowPosition : MonoBehaviour 
{
	void OnDrawGizmos()
	{
		Gizmos.DrawWireSphere(transform.position , 1);
	}
}