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
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('FC362DD2-54FC-4B96-B87F-72E21DBC4625', 'Delta', '8429 S. Wild Rose St., Montclair, NJ 07042')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('7fe1e239-6aa3-4c17-8c78-d134fc15d956', 'Epsilon', '425 Beechwood Lane, Virginia Beach, VA 23451')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('5d2635c2-bab3-42d0-9ed0-da6eb9799220', 'Zeta', '47 East Arnold St., Morganton, NC 28655')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('d212da19-93ed-444c-bdaa-c2efc864b82f', 'Eta', '20 Jackson Dr., Mc Lean, VA 22101')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('31e08408-2361-4c75-a144-4b9a9a613cea', 'Theta', '34 Westminster Street, Powder Springs, GA 30127')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('0e7b8ded-3a92-4606-b699-ec6f14190ef0', 'Iota', '105 Marsh Road, North Miami Beach, FL 33160')");
            migrationBuilder.Sql("INSERT INTO Locations (LocationId, Name, Address) VALUES ('8891a4d9-98b4-49d2-a1cf-67b229ad55c6', 'Kappa', '7169 Rockville St., North Ridgeville, OH 44039')");

            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('59a3989b-8d6e-4b11-b360-52c4f94159b9', 'Gema', 'Halliday', '28 West Buckingham Street, Bemidji, MN 56601', '721-555-2195', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('33cc1dec-2b88-472d-8798-1d37d8823f0e', 'Abdul', 'Seneca', '249 Paris Hill Street, Reisterstown, MD 21136', '616-555-2521', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('a53b4693-d6ac-4d84-bb0a-9340d21d4779', 'Marx', 'Eckley', '842 Hillside Rd., Pompano Beach, FL 33060', '312-555-0972', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('ae344f9d-29c6-4412-885b-f589873447ed', 'Leanna', 'Dibiase', '70 South University St., Oswego, NY 13126', '453-555-3116', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('01a326c5-a5f5-463d-99d5-ebb91adad452', 'Dong', 'Iles', '596 Pennsylvania Street, Fremont, OH 43420', '525-555-6283', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('a23a4dde-a0b7-4aa7-bc1a-bedc4d965d26', 'Melissa', 'Alfonso', '9966 N. Oxford Ave., Twin Falls, ID 83301', '697-555-6791', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('6118c5c8-2f44-4634-8370-0c5623438bb7', 'Olin', 'Gillooly', '292 Glenholme Drive, Circle Pines, MN 55014', '879-555-5982', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('653083a2-39a5-4d69-a615-3505710f3bb2', 'Tamica', 'Higgs', '8234 Edgefield Ave., Massapequa, NY 11758', '669-555-9601', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('e0e0b819-2d8d-484f-9013-aeb054f227b1', 'Sherry', 'Roa', '63 Bank Drive, Woodside, NY 11377', '211-555-9292', '123')");
            migrationBuilder.Sql("INSERT INTO Customers (CustomerId, FirstName, LastName, Address, PhoneNumber, Password) VALUES ('f91fa2f2-0e35-4476-ba81-da325cbe6c32', 'Hee', 'Primm', '7649 Arch Drive, Marietta, GA 30008', '284-555-3831', '123')");

            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('b52c8e10-a72b-4b74-80a3-7367a108bb46', '70', 'Chair')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('34d34e7b-5911-4805-af27-c0ee12c1eba7', '200', 'Table')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('c2f49c8c-1e38-444c-9454-69d19c42fcb7', '80', 'Stool')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('681b47f6-a668-4465-b97d-001ae2c14b6f', '200', 'Futon')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('8ee96987-4bd9-4bf4-9a61-08053b4ed64d', '400', 'Couch')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('67f25997-f989-41f3-931c-f54fe8172ec8', '50', 'Hammock')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('c686dc98-fd97-4073-857f-17dbc48f7ca8', '350', 'Bed')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('bcccb075-aa60-443c-a098-820e3b3aad65', '200', 'Corner Desk')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('6616320f-142a-41d6-b673-405779cac6ab', '80', 'Bean Bag')");
            migrationBuilder.Sql("INSERT INTO Products (ProductId, Price, Name) VALUES ('3c7076ee-4887-4481-8e37-0d6b6bbe2d86', '90', 'Lounge Chair')");

            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('957682d3-a189-481b-935f-640cafc31ebc', 'Chair', 'b52c8e10-a72b-4b74-80a3-7367a108bb46')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('f38d469f-766e-4163-a5d6-4747a748a14d', 'Table', '34d34e7b-5911-4805-af27-c0ee12c1eba7')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('45058e6c-0c37-4f66-9a4b-3781733a4720', 'Stool', 'c2f49c8c-1e38-444c-9454-69d19c42fcb7')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('157829e1-3a57-4db6-84ae-db382c23ccf5', 'Futon', '681b47f6-a668-4465-b97d-001ae2c14b6f')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('1fe4b537-8e5a-4e55-a8ad-f3a002f66b79', 'Couch', '8ee96987-4bd9-4bf4-9a61-08053b4ed64d')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('3fe2d7dc-f908-43be-9794-e0e930844d6e', 'Hammock', '67f25997-f989-41f3-931c-f54fe8172ec8')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('06e8c88c-00a3-4572-b0b4-e84226baf7a8', 'Bed', 'c686dc98-fd97-4073-857f-17dbc48f7ca8')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('83efa814-879b-4ff3-82d3-121b09cf1008', 'Corner Desk Piece 1', 'bcccb075-aa60-443c-a098-820e3b3aad65')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('952fbfb4-80cc-4acd-93f0-95d45ea33443', 'Corner Desk Piece 2', 'bcccb075-aa60-443c-a098-820e3b3aad65')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('99efd899-4634-4c55-9a58-48f0f8b47aaa', 'Bean Bag', '6616320f-142a-41d6-b673-405779cac6ab')");
            migrationBuilder.Sql("INSERT INTO ProductComponents (ProductComponentId, Name, ProductId) VALUES ('8c17e461-e164-466e-8b24-78fe4070620b', 'Lounge Chair', '3c7076ee-4887-4481-8e37-0d6b6bbe2d86')");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
