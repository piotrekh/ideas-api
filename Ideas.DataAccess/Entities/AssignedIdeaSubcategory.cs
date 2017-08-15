namespace Ideas.DataAccess.Entities
{
    public class AssignedIdeaSubcategory
    {
        public int IdeaId { get; set; }

        public int IdeaSubcategoryId { get; set; }

        //----- Navigation properties -----

        public Idea Idea { get; set; }

        public IdeaSubcategory Subcategory { get; set; }
    }
}
