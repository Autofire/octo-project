using UnityEngine;
using NUnit.Framework;
using System.Linq;
using System;
using Level;

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
			expected.SequenceEqual(LevelLayout.GetAdjacentCoordinates(center))
		);
	}

	[TestCase(0, 0, 1, 0, true)]
	[TestCase(-1, 0, 1, 0, false)]
	[TestCase(1, 1, 1, 0, true)]
	[TestCase(8, 0, 1, 0, false)]
	[TestCase(0, 0, 0, 0, false)]
	public void TestAdjacencyCheck(int x1, int y1, int x2, int y2, bool expected) {
		Vector2Int loc1 = new Vector2Int(x1, y1);
		Vector2Int loc2 = new Vector2Int(x2, y2);

		Assert.AreEqual(LevelLayout.AreAdjacent(loc1, loc2), expected);
	}

	[Test]
	public void TestCount() {
		LevelLayout layout = new LevelLayout();

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
		LevelLayout layout = new LevelLayout();

		Vector2Int location = new Vector2Int(x, y);
		IRoom room = layout.AddRoom(location);

		Assert.AreEqual(location, room.Position);
		Assert.AreEqual(room, layout.GetRoom(location));

	}

	[Test]
	public void TestAdjacentRooms() {
		LevelLayout layout = new LevelLayout();

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

		IRoom[] results = layout.GetAdjacent(new Vector2Int(4, 3));

		Assert.AreEqual(
			results[(int) Direction.Up].Position,
			new Vector2Int(4, 4)
		);

		Assert.AreEqual(
			results[(int) Direction.Left].Position,
			new Vector2Int(3, 3)
		);

		Assert.AreEqual(
			results[(int) Direction.Right].Position,
			new Vector2Int(5, 3)
		);

		Assert.IsNull(
			results[(int) Direction.Down]
		);
	}

	[TestCase(0,0, 1,0, Direction.Right, Direction.Left)]
	[TestCase(1,0, 0,0, Direction.Left, Direction.Right)]
	[TestCase(0,0, 0,1, Direction.Up, Direction.Down)]
	[TestCase(0,1, 0,0, Direction.Down, Direction.Up)]
	public void TestConnectRoomsValid(
			int x1, int y1,
			int x2, int y2,
			Direction dirFrom1,
			Direction dirFrom2
	) {

		LevelLayout layout = new LevelLayout();
		Vector2Int loc1 = new Vector2Int(x1, y1); //Vector2Int.zero;
		Vector2Int loc2 = new Vector2Int(x2, y2); //Vector2Int.right;

		bool[] expected1 = new bool[(int) Direction.Count];
		expected1[(int) dirFrom1] = true;

		bool[] expected2 = new bool[(int) Direction.Count];
		expected2[(int) dirFrom2] = true;

		layout.AddRoom(loc1);
		layout.AddRoom(loc2);
		layout.ConnectRooms(loc1, loc2);

		Assert.IsTrue(expected1.SequenceEqual(layout.GetRoom(loc1).Openings));
		Assert.IsTrue(expected2.SequenceEqual(layout.GetRoom(loc2).Openings));
	}

	[Test]
	public void TestConnectRoomsRedundant() {

		LevelLayout layout = new LevelLayout();
		Vector2Int loc1 = Vector2Int.zero;
		Vector2Int loc2 = Vector2Int.right;

		bool[] expected1 = new bool[4];
		expected1[(int) Direction.Right] = true;

		bool[] expected2 = new bool[4];
		expected2[(int) Direction.Left] = true;

		layout.AddRoom(loc1);
		layout.AddRoom(loc2);
		layout.ConnectRooms(loc1, loc2);
		layout.ConnectRooms(loc1, loc2);

		Assert.IsTrue(expected1.SequenceEqual(layout.GetRoom(loc1).Openings));
		Assert.IsTrue(expected2.SequenceEqual(layout.GetRoom(loc2).Openings));
	}

	[Test]
	public void TestConnectRoomsSeparate() {
		LevelLayout layout = new LevelLayout();
		Vector2Int loc1 = Vector2Int.zero;
		Vector2Int loc2 = Vector2Int.one;

		layout.AddRoom(loc1);
		layout.AddRoom(loc2);

		try {
			layout.ConnectRooms(loc1, loc2);
			Assert.Fail("Connecting separate rooms should have thrown an exception");
		}
		catch(ArgumentOutOfRangeException) {

		}
	}

	[Test]
	public void TestConnectRoomsMissingOne() {
		LevelLayout layout = new LevelLayout();
		Vector2Int loc1 = Vector2Int.zero;
		Vector2Int loc2 = Vector2Int.right;

		layout.AddRoom(loc1);

		try {
			layout.ConnectRooms(loc1, loc2);
			Assert.Fail("Connecting a room to a null room should have thrown an exception");
		}
		catch(ArgumentOutOfRangeException) {

		}
	}

	[Test]
	public void TestConnectRoomsMissingBoth() {
		LevelLayout layout = new LevelLayout();
		Vector2Int loc1 = Vector2Int.zero;
		Vector2Int loc2 = Vector2Int.right;

		try {
			layout.ConnectRooms(loc1, loc2);
			Assert.Fail("Both null rooms should have thrown an exception");
		}
		catch(ArgumentOutOfRangeException) {

		}
	}

	[Test]
	public void TestRoomsWithMissingNeighbors() {
		LevelLayout layout = new LevelLayout();

		layout.AddRoom(Vector2Int.zero);
		layout.AddRoom(Vector2Int.up);
		layout.AddRoom(Vector2Int.down);
		layout.AddRoom(Vector2Int.left);
		layout.AddRoom(Vector2Int.right);

		Vector2Int[] result = layout.RoomsWithMissingNeighbors();
		Assert.IsTrue(!result.Contains(Vector2Int.zero));
		Assert.IsTrue(result.Contains(Vector2Int.up));
		Assert.IsTrue(result.Contains(Vector2Int.down));
		Assert.IsTrue(result.Contains(Vector2Int.left));
		Assert.IsTrue(result.Contains(Vector2Int.right));
	}
}
