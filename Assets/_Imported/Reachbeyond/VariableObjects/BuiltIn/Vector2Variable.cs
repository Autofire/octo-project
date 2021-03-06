//
//  Vector2Variable.cs
//
//  Author:
//       Autofire <http://www.reach-beyond.pro/>
//
//  Copyright (c) 2018 ReachBeyond
//
//  This program is free software: you can redistribute it and/or modify
//  it under the terms of the GNU Lesser General Public License as published by
//  the Free Software Foundation, either version 3 of the License, or
//  (at your option) any later version.
//
//  This program is distributed in the hope that it will be useful,
//  but WITHOUT ANY WARRANTY; without even the implied warranty of
//  MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
//  GNU Lesser General Public License for more details.
//
//  You should have received a copy of the GNU Lesser General Public License
//  along with this program.  If not, see <http://www.gnu.org/licenses/>.


using UnityEngine;

namespace ReachBeyond.VariableObjects {

	[CreateAssetMenu(menuName="Variable/Vector2", order = 350060)]
	public class Vector2Variable : Base.StructVariable<Vector2> {}

	[System.Serializable]
	public class Vector2Reference : Base.Reference<Vector2, Vector2Variable> {}

	[System.Serializable]
	public class Vector2ConstReference : Base.ConstReference<Vector2, Vector2Variable> {}

}


/* DO NOT REMOVE -- START VARIABLE OBJECT INFO -- DO NOT REMOVE **
{
    "name": "Vector2",
    "type": "Vector2",
    "referability": "Struct",
    "menuOrder": 350060,
    "builtin": true
}
** DO NOT REMOVE --  END VARIABLE OBJECT INFO  -- DO NOT REMOVE */
