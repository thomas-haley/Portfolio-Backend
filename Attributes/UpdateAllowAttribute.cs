namespace Portfolio_Backend.Attributes;


/**
UpdateAllowAttribute signifies a property to be updated when server recieves updated entity DTO to ensure no
overwritting of signifigant information (ex. Created dates, id's, etc.)
*/
[AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
public class UpdateAllowAttribute : Attribute{

}