using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FELISZEWSKI_GISWI_LAB01
{
	class Program
	{
		static string path = @"C:\TEMP\GISWI";

		static void printVertices(List<Tuple<int, int>> vertices, ref List<int> edges)
		{

			foreach (var tuple in vertices)
			{
				if (!edges.Contains(tuple.Item1))
				{
					edges.Add(tuple.Item1);
				}
				if (!edges.Contains(tuple.Item2))
				{
					edges.Add(tuple.Item2);
				}
			}
			Console.WriteLine("Liczba wierzcholkow grafu G wynosi: " + edges.Count);
			Console.WriteLine("Zbior wierzcholkow V = {" + string.Join(", ", edges) + "}");
		}
		static void printEdges(List<Tuple<int, int>> vertices, int edges)
		{
			Console.WriteLine("Liczba krawedzi grafu G wynosi: " + edges);
			Console.WriteLine("Zbior krawedzi E = {" + string.Join(", ", vertices) + "}");
		}

		static void printMatrix(int[,] matrix)
		{
			int rowLenght = matrix.GetLength(0);
			int colLenght = matrix.GetLength(1);
			Console.WriteLine("Macierz sasiedztwa A =");
			for (int i = 0; i < rowLenght; i++)
			{
				Console.Write("| ");
				for (int j = 0; j < colLenght; j++)
					Console.Write(matrix[i, j] + " ");
				Console.Write("|" + Environment.NewLine);
			}
		}


		static void IncidenceMatrix(out int[,] matrix, List<Tuple<int, int>> vertices, int verticesNumber, int edgesNumber)
		{
			matrix = new int[verticesNumber, edgesNumber];
			for (int i = 0; i < verticesNumber; i++)
				for (int j = 0; j < edgesNumber; j++)
				{
					Tuple<int, int> tuple = Tuple.Create(i + 1, j + 1);
					int index = vertices.IndexOf(tuple);
					if (index != -1)
					{
						matrix[i, index] = 1;
					}
					else
					{
						Tuple<int, int> tupleAlter = Tuple.Create(j + 1, i + 1);
						int indexAlter = vertices.IndexOf(tupleAlter);
						if (indexAlter != -1)
						{
							matrix[i, indexAlter] = 1;
						}
					}
				}
		}

		static void AdjacencyMatrix(out int[,] matrix, List<Tuple<int, int>> vertices, int verticesNumber)
		{

			matrix = new int[verticesNumber, verticesNumber];
			for (int i = 0; i < verticesNumber; i++)
				for (int j = 0; j < verticesNumber; j++)
				{
					int count = 0;
					foreach (var vertice in vertices)
					{
						if ((vertice.Item1 == i + 1 && vertice.Item2 == j + 1) || (vertice.Item2 == i + 1 && vertice.Item1 == j + 1))
						{ count++; }
					}
					matrix[i, j] = count;
				}
		}

		static string getFilePath()
		{
			string fileName;
			while (true)
			{
				Console.WriteLine("Podaj nazwe pliku tekstowego w " + path + " z rozszerzeniem: ");
				fileName = Console.ReadLine();
				if (!string.IsNullOrEmpty(fileName) && File.Exists(path + "\\" + fileName))
				{
					return path + "\\" + fileName;
				}
			}
		}

		static Dictionary<int, int> GetDegrees(List<Tuple<int, int>> vertices, List<int> edges)
		{
			Dictionary<int, int> degrees = new Dictionary<int, int>();
			foreach (var edge in edges)
			{
				int degree = 0;
				foreach (var tupple in vertices)
				{
					if ((tupple.Item1 == edge) && (tupple.Item2 == edge))
					{
						degree += 2;
					}
					else if ((tupple.Item1 == edge) || (tupple.Item2 == edge))
					{
						degree++;
					}
				}
				degrees.Add(edge, degree);
			}
			Console.WriteLine();
			Console.WriteLine("Stopnie wierzcholkow:");
			foreach (var degree in degrees)
			{
				Console.WriteLine("deg(" + degree.Key + ") = " + degree.Value);
			}
			Console.WriteLine();
			return degrees;
		}

		static List<int> getDegreeSeries(Dictionary<int, int> degrees)
		{
			List<int> degreeSeries = new List<int>();

			foreach (var degreePair in degrees)
			{
				degreeSeries.Add(degreePair.Value);
			}
			degreeSeries.Sort();
			return degreeSeries;
		}

		static void Lab1Exercise2()
		{
			string fileName = getFilePath();
			BaseGraph baseGraph = new BaseGraph(fileName);
			int verticesNumer = baseGraph.GetVerticesNumer;
			int edgesNumber = baseGraph.GetEdgesNumber;
			Console.WriteLine("Rzad grafu G wynosi: " + verticesNumer);
			Console.WriteLine("Rozmiar grafu G wynosi: " + edgesNumber);
			List<Tuple<int, int>> vertices = baseGraph.GetVertices;
			List<int> edges = baseGraph.GetEdges;
			Dictionary<int, int> degrees = GetDegrees(vertices, edges);
			List<int> degreeSeries = getDegreeSeries(degrees);
			string graphDegrees = "";
			foreach (var degree in degreeSeries)
			{
				graphDegrees += degree + ", ";
			}
			graphDegrees.TrimEnd(',');
			Console.WriteLine("Ciag stopni grafu G: " + graphDegrees);
			Console.WriteLine("Wcisnij ENTER");
			Console.ReadLine();
		}

		static bool IsGrraphHaveLoops(List<Tuple<int, int>> vertices)
		{
			foreach (var vertice in vertices)
			{
				if (vertice.Item1 == vertice.Item2)
				{
					return true;

				}
			}
			return false;
		}

		static bool IsGraphHaveMultiVertices(List<Tuple<int, int>> vertices)
		{
			foreach (var vertice in vertices)
			{
				//if(vertice.Item1!=vertice.Item2)
				//{
				int verticeCount = 0;
				foreach (var verticeLoop in vertices)
				{
					if ((verticeLoop.Item1 == vertice.Item1 && verticeLoop.Item2 == vertice.Item2) || (verticeLoop.Item1 == vertice.Item2 && verticeLoop.Item2 == vertice.Item1))
					{
						verticeCount++;
					}
					if (verticeCount > 1)
					{
						return true;
					}
				}
				if (verticeCount > 1)
				{
					return true;
				}
				//}
			}
			return false;
		}

		static void Lab1Exercise3()
		{
			string fileName = getFilePath();
			BaseGraph baseGraph = new BaseGraph(fileName);
			List<Tuple<int, int>> vertices = baseGraph.GetVertices;
			bool isGraphHaveLoops = false;
			bool isgraphHaveMultiVertices = false;
			isGraphHaveLoops = IsGrraphHaveLoops(vertices);
			if (!isGraphHaveLoops)
			{
				isgraphHaveMultiVertices = IsGraphHaveMultiVertices(vertices);
			}
			if (!isGraphHaveLoops && !isgraphHaveMultiVertices)
			{
				Console.WriteLine("Graf G jest grafem prostym");
			}
			else
			{
				Console.WriteLine("Graf G jest prafem ogolnym");
			}
			Console.WriteLine("Wcisnij ENTER");
			Console.ReadLine();
		}

		static void Lab1Exercise4()
		{
			string fileName = getFilePath();
			BaseGraph baseGraph = new BaseGraph(fileName);
			List<Tuple<int, int>> vertices = baseGraph.GetVertices;


			bool isGraphHaveLoops = false;
			bool isgraphHaveMultiVertices = false;
			isGraphHaveLoops = IsGrraphHaveLoops(vertices);
			if (!isGraphHaveLoops)
			{
				isgraphHaveMultiVertices = IsGraphHaveMultiVertices(vertices);
			}
			if (!isGraphHaveLoops && !isgraphHaveMultiVertices)
			{
				int verticesCount = baseGraph.GetVerticesNumer;
				List<int> edges = baseGraph.GetEdges;
				Dictionary<int, int> degrees = GetDegrees(vertices, edges);
				List<int> degreeSeries = getDegreeSeries(degrees);
				bool isFullGraph = true;
				foreach (var degree in degreeSeries)
				{
					if (degree != verticesCount - 1)
					{
						Console.WriteLine("Graf G nie jest grafem pelnym.");
						isFullGraph = false;
						break;
					}
				}
				if (!isFullGraph)
				{
					List<Tuple<int, int>> complementaryVertices = new List<Tuple<int, int>>();
					foreach (var edge in edges)
					{
						foreach (var edgeNumber in edges)
						{
							if (edgeNumber == edge)
							{
								continue;
							}
							if ((complementaryVertices.Contains(Tuple.Create(edge, edgeNumber))) || (complementaryVertices.Contains(Tuple.Create(edgeNumber, edge))))
							{
								continue;
							}
							bool isVerticeCorrect = false;
							foreach (var vertice in vertices)
							{
								if (((edge == vertice.Item1) && (edgeNumber == vertice.Item2)) || ((edge == vertice.Item2) && (edgeNumber == vertice.Item1)))
								{
									isVerticeCorrect = true;
									break;
								}
							}
							if (!isVerticeCorrect)
							{
								complementaryVertices.Add(Tuple.Create(edge, edgeNumber));
							}
						}
					}
					Console.WriteLine("Krawedzie uzupelnienia grafu G = {" + string.Join(", ", complementaryVertices) + "}");
				}
				else
				{
					Console.WriteLine("Graf G jest grafem pelnym.");
				}
			}
			else
			{
				Console.WriteLine("Graf G nie jest grafem prostym, wiec nie ma dopelnienia.");
			}
			Console.WriteLine("Wcisnij ENTER");
			Console.ReadLine();
		}

		static void Lab1Exercise5()
		{
			string fileName = getFilePath();
			BaseGraph baseGraph = new BaseGraph(fileName);
			List<Tuple<int, int>> vertices = baseGraph.GetVertices;
			List<int> edges = baseGraph.GetEdges;
			edges.Sort();
			Dictionary<int, List<int>> listSavedInMemory = new Dictionary<int, List<int>>();
			foreach (var edge in edges)
			{
				List<int> verticeNeighbors = new List<int>();
				foreach (var vertice in vertices)
				{
					if (edge == vertice.Item1)
					{
						verticeNeighbors.Add(vertice.Item2);
					}
					else if (edge == vertice.Item2)
					{
						verticeNeighbors.Add(vertice.Item1);
					}
				}
				listSavedInMemory.Add(edge, verticeNeighbors);
			}
			Console.WriteLine("Lista wierzolkow grafu G: ");
			foreach (var list in listSavedInMemory)
			{
				Console.WriteLine(list.Key + " -> " + string.Join(", ", list.Value));
			}
			Console.WriteLine("Wcisnij ENTER");
			Console.ReadLine();
		}

		static void Lab1Exercise1()
		{
			string fileName = getFilePath();
			BaseGraph baseGraph = new BaseGraph(fileName);

			List<Tuple<int, int>> vertices = baseGraph.GetVertices;
			int edgesNumber = baseGraph.GetEdgesNumber;
			int verticesNumer = baseGraph.GetVerticesNumer;
			List<int> edges = new List<int>();
			printVertices(vertices, ref edges);
			printEdges(vertices, edgesNumber);
			int[,] matrix;
			AdjacencyMatrix(out matrix, vertices, verticesNumer);
			printMatrix(matrix);
			IncidenceMatrix(out matrix, vertices, verticesNumer, edgesNumber);
			printMatrix(matrix);
			Console.WriteLine("Wcisnij ENTER");
			Console.ReadLine();
		}


		static void Main(string[] args)
		{
			int value = 1;
			while (true)
			{
				Console.Clear();
				Console.WriteLine("Wybierz zadanie z Listy 1");
				Console.WriteLine("1 - zadanie nr 1");
				Console.WriteLine("2 - zadanie nr 2");
				Console.WriteLine("3 - zadanie nr 3");
				Console.WriteLine("4 - zadanie nr 4");
				Console.WriteLine("5 - zadanie nr 5");
				Console.WriteLine("0 - wyjście");
				string typedValue = Console.ReadLine();
				if (!int.TryParse(typedValue, out value))
				{
					continue;
				}
				else
				{
					switch (value)
					{
						case 1:
							Lab1Exercise1();
							break;
						case 2:
							Lab1Exercise2();
							break;
						case 3:
							Lab1Exercise3();
							break;
						case 4:
							Lab1Exercise4();
							break;
						case 5:
							Lab1Exercise5();
							break;
						case 0:
							return;
					}
				}
			}
		}
	}
}
