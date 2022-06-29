using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed = 1f;
    [SerializeField] private float _stepSize = 0.7f;

	private Rigidbody2D _rb;

	private void Awake()
	{
		_rb = GetComponent<Rigidbody2D>();	
	}

	private void Update()
	{
		// TO DO:
		// Dynamic steps
		// Delete RigidBody ?

		Vector3 velocity = new Vector2(_speed * Input.GetAxis("Horizontal"), _speed * Input.GetAxis("Vertical"));
		_rb.MovePosition(transform.position + velocity * Time.deltaTime);
	}
}
