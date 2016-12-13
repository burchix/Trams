using System.Collections.Generic;
using Tram.Common.Models;

namespace Tram.Controller.Repositories
{
    public interface IRepository
    {
        void LoadData(params string[] parameters);

        List<TramsIntersection> GetTramsIntersections();

        List<Node> GetMap();

        List<TramLine> GetLines();
    }
}
