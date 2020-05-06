using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreDb.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name) VALUES ('B8B94F18-D101-4576-AF28-3CBF31EBD6B2', 'Choco Castle')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name) VALUES ('BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', 'Sweet Tooth')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name) VALUES ('DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', 'Candy Land')");

            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('59A3989B-8D6E-4B11-B360-52C4F94159B9', 'Gema', 'Halliday','721-555-2195', '123', 'gema@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('33CC1DEC-2B88-472D-8798-1D37D8823F0E', 'Abdul', 'Seneca', '616-555-2521', '123', 'abdul@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('A53B4693-D6AC-4D84-BB0A-9340D21D4779', 'Marx', 'Eckley', '312-555-0972', '123', 'marx@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('AE344F9D-29C6-4412-885B-F589873447ED', 'Leanna', 'Dibiase', '453-555-3116', '123', 'leanna@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('01A326C5-A5F5-463D-99D5-EBB91ADAD452', 'Dan', 'Iles', '525-555-6283', '123', 'dan@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('A23A4DDE-A0B7-4AA7-BC1A-BEDC4D965D26', 'Melissa', 'Alfonso', '697-555-6791', '123', 'melissa@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('6118C5C8-2F44-4634-8370-0C5623438BB7', 'Olin', 'Gillooly', '879-555-5982', '123', 'olin@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('653083A2-39A5-4D69-A615-3505710F3BB2', 'Tamica', 'Higgs', '669-555-9601', '123', 'tamica@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('E0E0B819-2D8D-484F-9013-AEB054F227B1', 'Sherry', 'Roa', '211-555-9292', '123', 'sherry@example.com')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, PhoneNumber, Password, Login) VALUES ('F91FA2F2-0E35-4476-BA81-DA325CBE6C32', 'Sara', 'Primm', '284-555-3831', '123', 'sara@example.com')");

            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('B52C8E10-A72B-4B74-80A3-7367A108BB46', '1', 'Lollipop')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('34D34E7B-5911-4805-AF27-C0EE12C1EBA7', '2', 'Milk Chocolate Bar')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('C2F49C8C-1E38-444C-9454-69D19C42FCB7', '2', 'Chocolate Bar with Almonds')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('681B47F6-A668-4465-B97D-001AE2C14B6F', '2', 'Dark Chocolate Bar')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('8EE96987-4BD9-4BF4-9A61-08053B4ED64D', '3', 'Bag of Gummy Bears')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('67F25997-F989-41F3-931C-F54FE8172EC8', '4', 'Bag of Sour Gummies')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('C686DC98-FD97-4073-857F-17DBC48F7CA8', '3', 'Rock Candy')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('BCCCB075-AA60-443C-A098-820E3B3AAD65', '2', 'Toffee')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('6616320F-142A-41D6-B673-405779CAC6AB', '1', 'Giant Gumball')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('3C7076EE-4887-4481-8E37-0D6B6BBE2D86', '1', 'Bag of Jelly Beans')");

            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('F2F69D85-D42D-48AD-84F0-A242C2490E67', 'B52C8E10-A72B-4B74-80A3-7367A108BB46', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '200')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('6C4C86E0-D692-43A7-B74F-31463BFFEC04', '34D34E7B-5911-4805-AF27-C0EE12C1EBA7', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '100')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('A08CDECA-C860-468B-8E12-2812D11C54F5', 'C2F49C8C-1E38-444C-9454-69D19C42FCB7', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '500')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('AFF3EE78-E6EC-427A-88BF-54BFD3A213DA', '681B47F6-A668-4465-B97D-001AE2C14B6F', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '400')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('76C43AAE-361F-4E35-9E90-6F96C8E0E18D', '8EE96987-4BD9-4BF4-9A61-08053B4ED64D', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '700')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('9254ACF8-6F45-4E3C-9B1D-345FFC56AFD2', '67F25997-F989-41F3-931C-F54FE8172EC8', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '300')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('E1AB6F82-102D-41C4-8ABC-3364A4991C1A', 'C686DC98-FD97-4073-857F-17DBC48F7CA8', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '220')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('07F17ADF-3EE2-49BB-B0C5-AC4A06EB7CF7', 'BCCCB075-AA60-443C-A098-820E3B3AAD65', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '190')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('2DF43FB2-7319-4A15-B05F-FC1ACA5CDD44', '6616320F-142A-41D6-B673-405779CAC6AB', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '120')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('E2E26F8C-2305-46A7-B15C-711EACF27746', '3C7076EE-4887-4481-8E37-0D6B6BBE2D86', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '440')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('25AD245A-546A-4CF2-8572-CFD61EA2774C', 'B52C8E10-A72B-4B74-80A3-7367A108BB46', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '550')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('BF88AE40-A339-4128-99C7-2C557B428B79', '34D34E7B-5911-4805-AF27-C0EE12C1EBA7', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '120')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('7903DBBF-5676-45FE-BD9C-86E55BB3D40D', 'C2F49C8C-1E38-444C-9454-69D19C42FCB7', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '100')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('679A204D-8F1B-4C8A-90FA-5C654FFC9616', '681B47F6-A668-4465-B97D-001AE2C14B6F', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '400')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('A86DFEA1-6C11-4481-BA5E-AD2CFACC8BF0', '8EE96987-4BD9-4BF4-9A61-08053B4ED64D', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '220')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('3BFCB4DB-2D35-49C2-9014-93E8C1DA7A18', '67F25997-F989-41F3-931C-F54FE8172EC8', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '670')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('DB22E0EC-C535-4E91-AABE-AF8BD2812CC1', 'C686DC98-FD97-4073-857F-17DBC48F7CA8', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '770')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('CDD2337F-9CAA-4D06-B34C-E6FB2889EBFA', 'BCCCB075-AA60-443C-A098-820E3B3AAD65', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '880')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('DFBBD5B9-F0E9-4E85-887F-F2E5F11DFF09', '6616320F-142A-41D6-B673-405779CAC6AB', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '400')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('C246B2DA-F9CB-43EA-8EAB-5FB1B38C7FF1', '3C7076EE-4887-4481-8E37-0D6B6BBE2D86', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '320')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('1F189DE2-304E-4834-BF21-1503FE46EF41', 'B52C8E10-A72B-4B74-80A3-7367A108BB46', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '600')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('A19C268B-C65C-4A97-9B05-CDB2E6FF077B', '34D34E7B-5911-4805-AF27-C0EE12C1EBA7', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '300')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('CDB0801B-BE91-4900-8B65-6164DD0116F4', 'C2F49C8C-1E38-444C-9454-69D19C42FCB7', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '250')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('16E19FDF-9C17-4F00-BF3A-916A83D88792', '681B47F6-A668-4465-B97D-001AE2C14B6F', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '280')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('34D901B7-C46A-4309-B936-BBF2F00A5706', '8EE96987-4BD9-4BF4-9A61-08053B4ED64D', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '390')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('40BD668A-CBDC-43D3-B62E-EFFB07ED7E2B', '67F25997-F989-41F3-931C-F54FE8172EC8', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '320')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('21BA560A-2517-46CB-AD95-97192CE17F1F', 'C686DC98-FD97-4073-857F-17DBC48F7CA8', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '330')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('9764F044-1C28-4309-AFA2-EF5A21835C6A', 'BCCCB075-AA60-443C-A098-820E3B3AAD65', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '270')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('0939A83A-00A6-46DC-8F06-95C02ACCDBA8', '6616320F-142A-41D6-B673-405779CAC6AB', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '170')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductId, LocationId, Quantity) VALUES ('FFA610A8-EFA5-40EF-BFAE-4FEC12464981', '3C7076EE-4887-4481-8E37-0D6B6BBE2D86', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '220')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
