using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FELISZEWSKI_GISWI_LAB01
{
	public class BaseGraph
	{
		private int edgesNumber;
		private int verticesNumer;
		private List<Tuple<int, int>> vertices;
		private List<int> edges;
		public int GetEdgesNumber
		{
			get
			{
				return edgesNumber;
			}
		}
		public int GetVerticesNumer
		{
			get
			{
				return verticesNumer;
			}
		}

		public List<Tuple<int, int>> GetVertices
		{
			get
			{
				return vertices.ToList();
			}
		}

		public List<int> GetEdges
		{
			get
			{
				return edges.ToList();
			}
		}

		private List<int> GetEdgesFromVerticesTuple(List<Tuple<int, int>> vertices)
		{
			List<int> edgesTemp = new List<int>();
			foreach (var tuple in vertices)
			{
				if (!edgesTemp.Contains(tuple.Item1))
				{
					edgesTemp.Add(tuple.Item1);
				}
				if (!edgesTemp.Contains(tuple.Item2))
				{
					edgesTemp.Add(tuple.Item2);
				}
			}
			return edgesTemp;
		}


		public BaseGraph(string fileName)
		{

			vertices = GetTuppleVerticesFromFile(fileName, out edgesNumber, out verticesNumer);
			edges = GetEdgesFromVerticesTuple(vertices);

		}

		private List<Tuple<int, int>> GetTuppleVerticesFromFile(string fileName, out int edgesNumber, out int verticesNumer)
		{
			edgesNumber = 0;
			verticesNumer = 0;
			List<Tuple<int, int>> vertices = new List<Tuple<int, int>>();
			try
			{
				using (StreamReader streamReader = new StreamReader(fileName))
				{
					string[] headers = streamReader.ReadLine().Split(' ');
					if (headers.Length == 2)
					{
						verticesNumer = int.Parse(headers[0]);
						edgesNumber = int.Parse(headers[1]);
						string line;
						while ((line = streamReader.ReadLine()) != null)
						{
							string[] values = line.Split(' ');
							if (values.Length == 2)
							{
								vertices.Add(Tuple.Create(int.Parse(values[0]), int.Parse(values[1])));
							}
							else
							{
								throw new IOException("Reading file failed");
							}
						}
					}
				}
			}
			catch (IOException ex)
			{
				Console.WriteLine("Failet during reading" + Environment.NewLine + ex.Message);
				vertices = null;
			}
			return vertices;
		}
	}
}
