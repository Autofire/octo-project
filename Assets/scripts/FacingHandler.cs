using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class FacingHandler : MonoBehaviour
{
#pragma warning disable CS0649
	[SerializeField] private Vector2 _facing = Vector2.down;

	[Header("Animation setup")]
	[SerializeField] private Animator anim;
	[SerializeField] private string xField = "FaceX";
	[SerializeField] private string yField = "FaceY";

	[Space(5)]
	[SerializeField] private SpriteRenderer sprite;
	[SerializeField] private bool flipOnLeft = true;

	public Vector2 Facing {
		get {
			return _facing;
		}

		set {
			_facing = value;

			if(Mathf.Abs(_facing.x) < Mathf.Abs(_facing.y)) {
				_facing.x = 0;
			}
			else {
				_facing.y = 0;
			}

			ApplyFacing();
		}
	}
#pragma warning restore CS0649

	private void Awake() {
		Assert.IsNotNull(anim);
		Assert.IsNotNull(sprite);
	}

#if UNITY_EDITOR
	private void OnValidate() {
		if(UnityEditor.EditorApplication.isPlaying) {
			ApplyFacing();
		}
	}
#endif

	private void OnEnable() {
		ApplyFacing();
	}

	/// <summary>
	/// Applies the current facing, not changing this.
	/// It gets called automatically when the facing is changed,
	/// but if something happens that causes the renderer to
	/// lose our current facing, we'll have to reset it.
	/// </summary>
	public void ApplyFacing() {
		if(anim.isActiveAndEnabled) {
			anim.SetFloat(xField, Facing.x);
			anim.SetFloat(yField, Facing.y);

			if(flipOnLeft) {
				sprite.flipX = (Facing.x < 0);
			}
		}
	}

}
