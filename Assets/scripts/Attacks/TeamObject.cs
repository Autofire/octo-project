using UnityEngine;
using System.Linq;

namespace Attacks {
	[CreateAssetMenu(fileName = "New Team", menuName = "Team")]
	public class TeamObject : ScriptableObject {

#pragma warning disable CS0649
		[SerializeField] private TeamObject[] _against;
#pragma warning restore CS0649

		/// <summary>
		/// Check if we're hostile to another team.
		/// 
		/// Note that this is not commutative
		/// (i.e. a.IsAgainst(b) may not be the same
		/// as b.IsAgainst(a) in every situation).
		/// </summary>
		/// <param name="other">The team we're checking.</param>
		/// <returns>True if we're hostile.</returns>
		public bool IsAgainst(TeamObject other) {
			return _against.Contains(other);
		}

	}
}

