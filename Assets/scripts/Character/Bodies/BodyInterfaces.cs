using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Characters.Bodies.Interfaces {

	/// <summary>
	/// The base type of all body classes. Implementing this means nothing on its own,
	/// but it is an indication of more specific body interfaces.
	/// </summary>
	public interface IBody { }


	/// <summary>
	/// Implementors can move around with a constant speed in any direction.
	/// </summary>
	public interface IWalk : IBody {

		/// <summary>
		/// The maximum walk speed.
		/// </summary>
		float WalkSpeed { get; }

		/// <summary>
		/// Move the character in a given direction.
		/// 
		/// If the magnitude of the movement vector is equal to or greater than one,
		/// then the character moves at their max speed.
		///
		/// This should be called every update frame. If no direction is desired,
		/// Vector2.zero should be passed in. If it does not get called this way,
		/// then the behaviour is undefined and depends on the implementation.
		/// </summary>
		/// <param name="direction">Direction to walk in.</param>
		void Walk(Vector2 direction);
	}

	public interface IAttack : IBody {

		/// <summary>
		/// The range of the attack.
		/// </summary>
		float AttackRange { get; }

		/// <summary>
		/// Attack in the previous direction.
		/// </summary>
		void Attack();

		/// <summary>
		/// Attack in the given direction.
		/// </summary>
		/// <param name="direction">The direction to aim the attack in.</param>
		void Attack(Vector2 direction);
	}
}
