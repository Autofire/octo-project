using UnityEngine;
using NUnit.Framework;
using System.Linq;
using System;

[TestFixture]
public class LayoutGeneratorTests {

	[TestCase(0, 0)]
	[TestCase(1, 1)]
	[TestCase(30, 0)]
	[TestCase(-1, 0)]
	[TestCase(0, 1)]
	[TestCase(0, -400)]
	[TestCase(-8, -1)]
	[TestCase(4, 3)]
	[TestCase(-506346, 85434)]
	public void TestAdjacentCoordinates(int xOffset, int yOffset) {
		Vector2Int offset = new Vector2Int(xOffset, yOffset);

		Vector2Int baseCenter = Vector2Int.zero;
		Vector2Int[] baseExpected = new Vector2Int[] { Vector2Int.up, Vector2Int.down, Vector2Int.right, Vector2Int.left };

		Vector2Int center = baseCenter + offset;

		Vector2Int[] expected = new Vector2Int[baseExpected.Length];
		for(int i = 0; i < expected.Length; i++) {
			expected[i] = baseExpected[i] + offset;
		}

		Assert.IsTrue(
			expected.SequenceEqual(LayoutGenerator.Layout.GetAdjacentCoordinates(center))
		);
	}

	[Test]
	public void TestCount() {
		LayoutGenerator.Layout layout = new LayoutGenerator.Layout();

		Assert.AreEqual(layout.Count, 0);

		layout.AddRoom(new Vector2Int(0, 0));
		Assert.AreEqual(layout.Count, 1);

		layout.AddRoom(new Vector2Int(0, 1));
		layout.AddRoom(new Vector2Int(0, 2));
		layout.AddRoom(new Vector2Int(0, 3));
		Assert.AreEqual(layout.Count, 4);
	}

	[TestCase(0, 0)]
	[TestCase(1, 1)]
	[TestCase(-30, 50)]
	public void TestAddRoom(int x, int y) {
		LayoutGenerator.Layout layout = new LayoutGenerator.Layout();

		Vector2Int location = new Vector2Int(x, y);
		LayoutGenerator.Room room = layout.AddRoom(location);

		Assert.AreEqual(location, room.Position);

		try {
			layout.AddRoom(new Vector2Int(x, y));
			Assert.Fail("LayoutGenerator.Layout.AddRoom didn't throw an ArgumentException");
		}
		catch(ArgumentException) {
			
		}
	}

	[Test]
	public void TestAdjacentRooms() {
		LayoutGenerator.Layout layout = new LayoutGenerator.Layout();

		// Checking at !, rooms at X
		//
		// 4       X
		// 3     X ! X 
		// 2   X
		// 1
		// 0 1 2 3 4 5

		layout.AddRoom(new Vector2Int(4, 4));
		layout.AddRoom(new Vector2Int(3, 3));
		layout.AddRoom(new Vector2Int(5, 3));
		layout.AddRoom(new Vector2Int(2, 2));

		LayoutGenerator.Room[] results = layout.GetAdjacent(new Vector2Int(4, 3));

		Assert.AreEqual(
			results[(int)LayoutGenerator.Direction.North].Position,
			new Vector2Int(4, 4)
		); 

		Assert.AreEqual(
			results[(int)LayoutGenerator.Direction.West].Position,
			new Vector2Int(3, 3)
		); 

		Assert.AreEqual(
			results[(int)LayoutGenerator.Direction.East].Position,
			new Vector2Int(5, 3)
		); 

		Assert.IsNull(
			results[(int)LayoutGenerator.Direction.South]
		); 
	}
}
