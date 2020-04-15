using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FELISZEWSKI_GISWI_LAB01
{
	public class Edge
	{
		private int sourceVertice;
		private int targetVertice;
		public int SourceVertice
		{
			get
			{
				return sourceVertice;
			}
		}
		public int TargetVertice
		{
			get
			{
				return targetVertice;
			}
		}
		public Edge(int sourceVertice, int targetVertice)
		{
			this.targetVertice = targetVertice;
			this.sourceVertice = sourceVertice;
		}
	}
}
