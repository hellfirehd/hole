using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Pdsi.Hole.Tests
{
	[TestClass]
	public class InterpreterTests
	{
		[TestMethod]
		public void Positive_Integer()
		{
			Eval("3").Should().Be(3);
		}

		[TestMethod]
		public void Negative_Integer()
		{
			Eval("-10").Should().Be(-10);
		}

		[TestMethod]
		public void Addition()
		{
			Eval("1 + 2").Should().Be(3);
			Eval("5 + 3").Should().Be(8);
		}

		[TestMethod]
		public void Subtraction()
		{
			Eval("3 - 2").Should().Be(1);
			Eval("2 - 3").Should().Be(-1);
		}

		[TestMethod]
		public void Addition_and_Subtraction()
		{
			Eval("1 + 2 - 3").Should().Be(0);
			Eval("3 - 2 + 1").Should().Be(2);
		}

		[TestMethod]
		public void Multiplication()
		{
			Eval("3 * 4").Should().Be(12);
			Eval("5 * 5").Should().Be(25);
		}

		[TestMethod]
		public void Division()
		{
			Eval("12 / 3").Should().Be(4);
			Eval("4 / 2").Should().Be(2);
		}

		[TestMethod]
		public void Multiplication_and_Division()
		{
			Eval("7 * 4 / 2").Should().Be(14);
			Eval("8 / 4 * 3").Should().Be(6);
			Eval("10 * 4 * 2 * 3 / 8").Should().Be(30);
		}

		[TestMethod]
		public void Parenthesized_Expression()
		{
			Eval("(0)").Should().Be(0);
			Eval("(1 + 2)").Should().Be(3);
			Eval("(1 + 2) * 3").Should().Be(9);
			Eval("(1 + 2) / 3").Should().Be(1);
		}

		[TestMethod]
		public void Order_of_Operations()
		{
			Eval("2 + 7 * 4").Should().Be(30);
			Eval("7 - 9 / 3").Should().Be(4);
			Eval("14 + 2 * 3 - 6 / 2").Should().Be(17);
		}

		[TestMethod]
		public void Unary_Operators()
		{
			Eval("5 - - - + - 3").Should().Be(8);
			Eval("5 - - - + - (3 + 4) - +2").Should().Be(10);
		}

		[TestMethod]
		public void Pure_Craziness()
		{
			Eval("7 + 3 * (10 / (12 / (3 + 1) - 1))").Should().Be(22);
			Eval("7 + 3 * (10 / (12 / (3 + 1) - 1)) / (2 + 3) - 5 - 3 + (8)").Should().Be(10);
			Eval("7 + (((3 + 2)))").Should().Be(12);
			Eval("(1) + ((2)) * (((3)))").Should().Be(7);
		}

		private Int32 Eval(String expression) => new Interpreter(new Parser(new Lexer(expression))).Interpret();

	}
}
