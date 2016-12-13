using System.Linq;
using Tram.Common.Models;

namespace Tram.Common.Extensions
{
    public static class LineExtensions
    {
        public static Node.Next GetNextNode(this TramLine line, Node node)
        {
            if (node.Child != null)
            {
                return node.Child;
            }
            else if (node.Children != null && node.Children.Any())
            {
                return node.Children.Single(ch => line.MainNodes.Any(mn => mn.Equals(ch.Node)));
            }

            return null;
        }
    }
}
