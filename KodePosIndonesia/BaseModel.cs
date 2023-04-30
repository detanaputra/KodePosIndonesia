namespace KodePosIndonesia
{
    public abstract class BaseModel
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return Name;
        }
    }
}
