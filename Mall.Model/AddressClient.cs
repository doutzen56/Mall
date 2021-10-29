using Mall.Model.DTO;
using System.Collections.Generic;

namespace Mall.Model
{
    public class AddressClient
    {
        public static readonly List<AddressDTO> addressList = new List<AddressDTO>()
        {new  AddressDTO(){
                Id=1L,
                Address="武汉市洪山区凯乐桂圆 W号楼",
                Name="gerry",
                Phone="15855500000"
        },
        new  AddressDTO()
        {
                Id=1L,
                Address="武汉市洪山区凯乐桂圆 W2号楼",
                Name="gerry",
                Phone="1234569877"
    }
};

        public static AddressDTO FindById(long id)
        {
            foreach (AddressDTO addressDTO in addressList)
            {
                if (addressDTO.Id == id) return addressDTO;
            }
            return null;
        }
    }
}
