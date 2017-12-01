namespace BusinessLogic.Model.Dtos
{
    public class CityDto : DtoBase<int>
    {
        /// <summary>
        /// Наименование города.
        /// </summary>
        public string Name { get; set; }
    }
}
