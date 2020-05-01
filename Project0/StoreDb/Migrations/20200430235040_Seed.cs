using Microsoft.EntityFrameworkCore.Migrations;

namespace StoreDb.Migrations
{
    public partial class Seed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('B8B94F18-D101-4576-AF28-3CBF31EBD6B2', 'Alpha', '41 North Parker St., Upper Darby, PA 19082')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', 'Beta', '2 South Fairway Avenue, Marquette, MI 49855')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', 'Gamma', '65 Central Lane, Metairie, LA 70001')");

            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('59A3989B-8D6E-4B11-B360-52C4F94159B9', 'Gema', 'Halliday', '28 West Buckingham Street, Bemidji, MN 56601', '721-555-2195', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('33CC1DEC-2B88-472D-8798-1D37D8823F0E', 'Abdul', 'Seneca', '249 Paris Hill Street, Reisterstown, MD 21136', '616-555-2521', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('A53B4693-D6AC-4D84-BB0A-9340D21D4779', 'Marx', 'Eckley', '842 Hillside Rd., Pompano Beach, FL 33060', '312-555-0972', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('AE344F9D-29C6-4412-885B-F589873447ED', 'Leanna', 'Dibiase', '70 South University St., Oswego, NY 13126', '453-555-3116', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('01A326C5-A5F5-463D-99D5-EBB91ADAD452', 'Dong', 'Iles', '596 Pennsylvania Street, Fremont, OH 43420', '525-555-6283', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('A23A4DDE-A0B7-4AA7-BC1A-BEDC4D965D26', 'Melissa', 'Alfonso', '9966 N. Oxford Ave., Twin Falls, ID 83301', '697-555-6791', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('6118C5C8-2F44-4634-8370-0C5623438BB7', 'Olin', 'Gillooly', '292 Glenholme Drive, Circle Pines, MN 55014', '879-555-5982', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('653083A2-39A5-4D69-A615-3505710F3BB2', 'Tamica', 'Higgs', '8234 Edgefield Ave., Massapequa, NY 11758', '669-555-9601', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('E0E0B819-2D8D-484F-9013-AEB054F227B1', 'Sherry', 'Roa', '63 Bank Drive, Woodside, NY 11377', '211-555-9292', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('F91FA2F2-0E35-4476-BA81-DA325CBE6C32', 'Hee', 'Primm', '7649 Arch Drive, Marietta, GA 30008', '284-555-3831', '123')");

            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('B52C8E10-A72B-4B74-80A3-7367A108BB46', '70', 'Chair')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('34D34E7B-5911-4805-AF27-C0EE12C1EBA7', '200', 'Table')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('C2F49C8C-1E38-444C-9454-69D19C42FCB7', '80', 'Stool')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('681B47F6-A668-4465-B97D-001AE2C14B6F', '200', 'Futon')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('8EE96987-4BD9-4BF4-9A61-08053B4ED64D', '400', 'Couch')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('67F25997-F989-41F3-931C-F54FE8172EC8', '50', 'Hammock')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('C686DC98-FD97-4073-857F-17DBC48F7CA8', '350', 'Bed')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('BCCCB075-AA60-443C-A098-820E3B3AAD65', '200', 'Corner Desk')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('6616320F-142A-41D6-B673-405779CAC6AB', '80', 'Bean Bag')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('3C7076EE-4887-4481-8E37-0D6B6BBE2D86', '90', 'Lounge Chair')");

            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('957682D3-A189-481B-935F-640CAFC31EBC', 'Chair', 'B52C8E10-A72B-4B74-80A3-7367A108BB46')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('F38D469F-766E-4163-A5D6-4747A748A14D', 'Table', '34D34E7B-5911-4805-AF27-C0EE12C1EBA7')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('45058E6C-0C37-4F66-9A4B-3781733A4720', 'Stool', 'C2F49C8C-1E38-444C-9454-69D19C42FCB7')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('157829E1-3A57-4DB6-84AE-DB382C23CCF5', 'Futon', '681B47F6-A668-4465-B97D-001AE2C14B6F')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('1FE4B537-8E5A-4E55-A8AD-F3A002F66B79', 'Couch', '8EE96987-4BD9-4BF4-9A61-08053B4ED64D')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('3FE2D7DC-F908-43BE-9794-E0E930844D6E', 'Hammock', '67F25997-F989-41F3-931C-F54FE8172EC8')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('06E8C88C-00A3-4572-B0B4-E84226BAF7A8', 'Bed', 'C686DC98-FD97-4073-857F-17DBC48F7CA8')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('83EFA814-879B-4FF3-82D3-121B09CF1008', 'Corner Desk Piece 1', 'BCCCB075-AA60-443C-A098-820E3B3AAD65')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('952FBFB4-80CC-4ACD-93F0-95D45EA33443', 'Corner Desk Piece 2', 'BCCCB075-AA60-443C-A098-820E3B3AAD65')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('99EFD899-4634-4C55-9A58-48F0F8B47AAA', 'Bean Bag', '6616320F-142A-41D6-B673-405779CAC6AB')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('8C17E461-E164-466E-8B24-78FE4070620B', 'Lounge Chair', '3C7076EE-4887-4481-8E37-0D6B6BBE2D86')");

            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('F2F69D85-D42D-48AD-84F0-A242C2490E67', '957682D3-A189-481B-935F-640CAFC31EBC', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '20')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('6C4C86E0-D692-43A7-B74F-31463BFFEC04', 'F38D469F-766E-4163-A5D6-4747A748A14D', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '10')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('A08CDECA-C860-468B-8E12-2812D11C54F5', '45058E6C-0C37-4F66-9A4B-3781733A4720', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '50')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('AFF3EE78-E6EC-427A-88BF-54BFD3A213DA', '157829E1-3A57-4DB6-84AE-DB382C23CCF5', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '40')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('76C43AAE-361F-4E35-9E90-6F96C8E0E18D', '1FE4B537-8E5A-4E55-A8AD-F3A002F66B79', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '70')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('9254ACF8-6F45-4E3C-9B1D-345FFC56AFD2', '3FE2D7DC-F908-43BE-9794-E0E930844D6E', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '30')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('E1AB6F82-102D-41C4-8ABC-3364A4991C1A', '06E8C88C-00A3-4572-B0B4-E84226BAF7A8', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '22')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('07F17ADF-3EE2-49BB-B0C5-AC4A06EB7CF7', '83EFA814-879B-4FF3-82D3-121B09CF1008', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '19')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('2DF43FB2-7319-4A15-B05F-FC1ACA5CDD44', '952FBFB4-80CC-4ACD-93F0-95D45EA33443', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '12')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('E2E26F8C-2305-46A7-B15C-711EACF27746', '99EFD899-4634-4C55-9A58-48F0F8B47AAA', 'B8B94F18-D101-4576-AF28-3CBF31EBD6B2', '44')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('25AD245A-546A-4CF2-8572-CFD61EA2774C', '8C17E461-E164-466E-8B24-78FE4070620B', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '55')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('BF88AE40-A339-4128-99C7-2C557B428B79', '957682D3-A189-481B-935F-640CAFC31EBC', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '12')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('7903DBBF-5676-45FE-BD9C-86E55BB3D40D', 'F38D469F-766E-4163-A5D6-4747A748A14D', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '10')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('679A204D-8F1B-4C8A-90FA-5C654FFC9616', '45058E6C-0C37-4F66-9A4B-3781733A4720', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '40')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('A86DFEA1-6C11-4481-BA5E-AD2CFACC8BF0', '157829E1-3A57-4DB6-84AE-DB382C23CCF5', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '22')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('3BFCB4DB-2D35-49C2-9014-93E8C1DA7A18', '1FE4B537-8E5A-4E55-A8AD-F3A002F66B79', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '67')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('DB22E0EC-C535-4E91-AABE-AF8BD2812CC1', '3FE2D7DC-F908-43BE-9794-E0E930844D6E', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '77')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('CDD2337F-9CAA-4D06-B34C-E6FB2889EBFA', '06E8C88C-00A3-4572-B0B4-E84226BAF7A8', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '88')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('DFBBD5B9-F0E9-4E85-887F-F2E5F11DFF09', '83EFA814-879B-4FF3-82D3-121B09CF1008', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '40')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('C246B2DA-F9CB-43EA-8EAB-5FB1B38C7FF1', '952FBFB4-80CC-4ACD-93F0-95D45EA33443', 'BBD4B6EB-CF72-4313-9C92-BD1BE7CAF949', '32')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('1F189DE2-304E-4834-BF21-1503FE46EF41', '99EFD899-4634-4C55-9A58-48F0F8B47AAA', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '60')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('A19C268B-C65C-4A97-9B05-CDB2E6FF077B', '8C17E461-E164-466E-8B24-78FE4070620B', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '30')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('CDB0801B-BE91-4900-8B65-6164DD0116F4', '957682D3-A189-481B-935F-640CAFC31EBC', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '25')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('16E19FDF-9C17-4F00-BF3A-916A83D88792', 'F38D469F-766E-4163-A5D6-4747A748A14D', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '28')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('34D901B7-C46A-4309-B936-BBF2F00A5706', '45058E6C-0C37-4F66-9A4B-3781733A4720', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '39')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('40BD668A-CBDC-43D3-B62E-EFFB07ED7E2B', '157829E1-3A57-4DB6-84AE-DB382C23CCF5', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '32')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('21BA560A-2517-46CB-AD95-97192CE17F1F', '1FE4B537-8E5A-4E55-A8AD-F3A002F66B79', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '33')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('9764F044-1C28-4309-AFA2-EF5A21835C6A', '3FE2D7DC-F908-43BE-9794-E0E930844D6E', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '27')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('0939A83A-00A6-46DC-8F06-95C02ACCDBA8', '06E8C88C-00A3-4572-B0B4-E84226BAF7A8', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '17')");
            migrationBuilder.Sql("INSERT INTO LocationInventories (LocationInventoryId, ProductComponentId, LocationId, Quantity) VALUES ('FFA610A8-EFA5-40EF-BFAE-4FEC12464981', '83EFA814-879B-4FF3-82D3-121B09CF1008', 'DEA1BDEA-FB74-4372-A9D1-03BF26C8804D', '22')");

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}