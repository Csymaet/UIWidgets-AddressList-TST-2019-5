namespace AddressListApp
{
	public class Contact
	{
		public string Name { get; set; } = "未命名";
		public string Telephone { get; set; }
		public string Mail { get; set; }
		public string Other { get; set; }

		public Contact()
		{

		}
		public Contact(string name)
		{
			this.Name = name;
		}
	}
}
