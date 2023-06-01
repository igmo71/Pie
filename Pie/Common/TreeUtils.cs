namespace Pie.Common
{
    public class TreeUtils
    {
        public static List<N> GetNodes<S, N>(List<S> entities)
            where S : class, ISelfRefEntity
            where N : class, ITreeNode, new()
        {
            var nodes = new List<N>();
            var firstLevelEntities = entities.Where(e => e.ParentId == null).ToList();
            foreach (var entity in firstLevelEntities)
            {
                N node = GetNode<S, N>(entity, entities);
                nodes.Add(node);
            }
            return nodes;
        }

        public static N GetNode<S, N>(S entity, List<S> entities)
            where N : class, ITreeNode, new()
            where S : class, ISelfRefEntity
        {
            N rootNode = new();
            rootNode.Id = entity.Id;
            rootNode.Name = entity.Name;

            var children = entities.Where(e => e.ParentId == rootNode.Id).ToList();

            if (children != null && children.Count != 0)
            {
                rootNode.Children = new();
                foreach (var child in children)
                {
                    N node = GetNode<S, N>(child, entities);
                    rootNode.Children.Add(node);
                }
            }
            return rootNode;
        }

        //public static void GetFlatDictionary1<N>(List<N> nodes, Dictionary<Guid, string> result, string prefix = "")
        //    where N : class, ITreeNode, new()
        //{
        //    foreach (var item in nodes)
        //    {
        //        result.Add(item.Id, $"{prefix}{item.Name}");

        //        if (item.Children != null && item.Children.Any())
        //        {
        //            List<N> children = item.Children.Select(c => new N() { Id = c.Id, Name = c.Name, Children = c.Children }).ToList();

        //            GetFlatDictionary1(children, result, $"{prefix}{item.Name}/");
        //        }
        //    }
        //}

        //public static void GetFlatDictionary2<N>(List<N> nodes, Dictionary<Guid, string> result, string prefix = "")
        //    where N : class, ITreeNode, new()
        //{
        //    foreach (var item in nodes)
        //    {
        //        result.Add(item.Id, $"{prefix}{item.Name}");

        //        if (item.Children != null && item.Children.Any())
        //        {
        //            List<N> children = item.Children.Select(c => new N() { Id = c.Id, Name = c.Name, Children = c.Children }).ToList();

        //            GetFlatDictionary2(children, result, $"{prefix}-");
        //        }
        //    }
        //}

        public static void GetFlatDictionary<N>(List<N> nodes, Dictionary<Guid, string> result, bool includeParent = false, string prefix = "")
            where N : class, ITreeNode, new()
        {
            foreach (var item in nodes)
            {
                result.Add(item.Id, $"{prefix}{item.Name}");

                if (item.Children != null && item.Children.Any())
                {
                    List<N> children = item.Children.Select(c => new N() { Id = c.Id, Name = c.Name, Children = c.Children }).ToList();

                    string newPrefix = "";

                    if (includeParent)
                        newPrefix = $"{prefix}{item.Name}/";
                    else
                        newPrefix = $"{prefix}-";

                    GetFlatDictionary(children, result, includeParent, newPrefix);
                }
            }
        }
    }

    public interface ISelfRefEntity
    {
        public Guid Id { get; set; }
        public Guid? ParentId { get; set; }
        public string? Name { get; set; }
    }

    public interface ITreeNode
    {
        public Guid Id { get; set; }
        public string? Name { get; set; }
        public List<ITreeNode>? Children { get; set; }
    }
}
