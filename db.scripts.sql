
--
-- SQ_AspNetUserClaims
--
 CREATE SEQUENCE "QLBB"."SQ_AspNetUserClaims" MINVALUE 1 MAXVALUE 9999999999999999999999999999 INCREMENT BY 1 CACHE 20 NOORDER NOCYCLE;
--
-- __MigrationHistory
--
  CREATE TABLE "QLBB"."__MigrationHistory" 
   (	"MigrationId" NVARCHAR2(150) NOT NULL ENABLE,
	"ContextKey" NVARCHAR2(300) NOT NULL ENABLE,
	"Model" BLOB NOT NULL ENABLE,
	"ProductVersion" NVARCHAR2(32) NOT NULL ENABLE,
	CONSTRAINT "PK___MigrationHistory" PRIMARY KEY ("MigrationId","ContextKey") ENABLE
   );
--
-- AspNetRoles
--
  CREATE TABLE "QLBB"."AspNetRoles" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"Name" NVARCHAR2(256) NOT NULL ENABLE,
	CONSTRAINT "PK_AspNetRoles" PRIMARY KEY ("Id") ENABLE
   );
--
-- AspNetUserClaims
--
  CREATE TABLE "QLBB"."AspNetUserClaims" 
   (	"Id" NUMBER(10,0) NOT NULL ENABLE,
	"UserId" NVARCHAR2(128) NOT NULL ENABLE,
	"ClaimType" NCLOB,
	"ClaimValue" NCLOB,
	CONSTRAINT "PK_AspNetUserClaims" PRIMARY KEY ("Id") ENABLE
   );
--
-- AspNetUserLogins
--
  CREATE TABLE "QLBB"."AspNetUserLogins" 
   (	"LoginProvider" NVARCHAR2(128) NOT NULL ENABLE,
	"ProviderKey" NVARCHAR2(128) NOT NULL ENABLE,
	"UserId" NVARCHAR2(128) NOT NULL ENABLE,
	CONSTRAINT "PK_AspNetUserLogins" PRIMARY KEY ("LoginProvider","ProviderKey","UserId") ENABLE
   );
--
-- AspNetUserRoles
--
  CREATE TABLE "QLBB"."AspNetUserRoles" 
   (	"UserId" NVARCHAR2(128) NOT NULL ENABLE,
	"RoleId" NVARCHAR2(128) NOT NULL ENABLE,
	CONSTRAINT "PK_AspNetUserRoles" PRIMARY KEY ("UserId","RoleId") ENABLE
   );
--
-- AspNetUsers
--
  CREATE TABLE "QLBB"."AspNetUsers" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"Email" NVARCHAR2(256),
	"EmailConfirmed" NUMBER(1,0) NOT NULL ENABLE,
	"PasswordHash" NCLOB,
	"SecurityStamp" NCLOB,
	"PhoneNumber" NCLOB,
	"PhoneNumberConfirmed" NUMBER(1,0) NOT NULL ENABLE,
	"TwoFactorEnabled" NUMBER(1,0) NOT NULL ENABLE,
	"LockoutEndDateUtc" DATE,
	"LockoutEnabled" NUMBER(1,0) NOT NULL ENABLE,
	"AccessFailedCount" NUMBER(10,0) NOT NULL ENABLE,
	"UserName" NVARCHAR2(256) NOT NULL ENABLE,
	"representImage_Id" NVARCHAR2(128),
	CONSTRAINT "PK_AspNetUsers" PRIMARY KEY ("Id") ENABLE
   );
--
-- Bakery
--
  CREATE TABLE "QLBB"."Bakery" 
   (	"ID" NVARCHAR2(128) NOT NULL ENABLE,
	"Name" NCLOB,
	"Price" NUMBER(10,0) NOT NULL ENABLE,
	"ngaypost" DATE NOT NULL ENABLE,
	"VAT" BINARY_DOUBLE NOT NULL ENABLE,
	"count" NUMBER(10,0) NOT NULL ENABLE,
	"category_Id" NVARCHAR2(128),
	"images_Id" NVARCHAR2(128),
	CONSTRAINT "PK_Bakery" PRIMARY KEY ("ID") ENABLE
   );
--
-- Bill
--
  CREATE TABLE "QLBB"."Bill" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"Nameuser" NCLOB,
	"Addressuser" NCLOB,
	"SDT" NCLOB,
	"Email" NCLOB,
	"confirmEmail" NCLOB,
	"Totalprice" NUMBER(10,0) NOT NULL ENABLE,
	CONSTRAINT "PK_Bill" PRIMARY KEY ("Id") ENABLE
   );
--
-- Billdetails
--
  CREATE TABLE "QLBB"."Billdetails" 
   (	"iddetails" NVARCHAR2(128) NOT NULL ENABLE,
	"quality" NUMBER(10,0) NOT NULL ENABLE,
	"Bakery_ID" NVARCHAR2(128),
	"Bill_Id" NVARCHAR2(128),
	CONSTRAINT "PK_Billdetails" PRIMARY KEY ("iddetails") ENABLE
   );
--
-- Category
--
  CREATE TABLE "QLBB"."Category" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"Name" NCLOB,
	CONSTRAINT "PK_Category" PRIMARY KEY ("Id") ENABLE
   );
--
-- Image
--
  CREATE TABLE "QLBB"."Image" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"nameImage" NCLOB,
	"url" NCLOB,
	"width" NUMBER(10,0) NOT NULL ENABLE,
	"height" NUMBER(10,0) NOT NULL ENABLE,
	CONSTRAINT "PK_Image" PRIMARY KEY ("Id") ENABLE
   );
--
-- Introduction
--
  CREATE TABLE "QLBB"."Introduction" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"details" NCLOB,
	"title" NCLOB,
	"type" NUMBER(10,0) NOT NULL ENABLE,
	"Author_Id" NVARCHAR2(128),
	"image_Id" NVARCHAR2(128),
	CONSTRAINT "PK_Introduction" PRIMARY KEY ("Id") ENABLE
   );
--
-- Log
--
  CREATE TABLE "QLBB"."Log" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"logstring" NCLOB,
	"datetime" DATE NOT NULL ENABLE,
	"bakeryuser_Id" NVARCHAR2(128),
	CONSTRAINT "PK_Log" PRIMARY KEY ("Id") ENABLE
   );
--
-- News
--
  CREATE TABLE "QLBB"."News" 
   (	"ID" NVARCHAR2(128) NOT NULL ENABLE,
	"Title" NCLOB,
	"Body" NCLOB,
	"DatePost" DATE NOT NULL ENABLE,
	CONSTRAINT "PK_News" PRIMARY KEY ("ID") ENABLE
   );
--
-- Sale
--
  CREATE TABLE "QLBB"."Sale" 
   (	"ID" NVARCHAR2(128) NOT NULL ENABLE,
	"time1" DATE NOT NULL ENABLE,
	"time2" DATE NOT NULL ENABLE,
	"sale" BINARY_DOUBLE NOT NULL ENABLE,
	CONSTRAINT "PK_Sale" PRIMARY KEY ("ID") ENABLE
   );
--
-- Shop
--
  CREATE TABLE "QLBB"."Shop" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"address" NCLOB,
	"gmail" NCLOB,
	"Googlemapembded" NCLOB,
	"SDT" NCLOB,
	CONSTRAINT "PK_Shop" PRIMARY KEY ("Id") ENABLE
   );
--
-- Slider
--
  CREATE TABLE "QLBB"."Slider" 
   (	"Id" NVARCHAR2(128) NOT NULL ENABLE,
	"position" NUMBER(10,0) NOT NULL ENABLE,
	"image_Id" NVARCHAR2(128),
	CONSTRAINT "PK_Slider" PRIMARY KEY ("Id") ENABLE
   );
--
-- TR_AspNetUserClaims
--
  CREATE OR REPLACE TRIGGER "QLBB"."TR_AspNetUserClaims"
  BEFORE INSERT ON "QLBB"."AspNetUserClaims"FOR EACH ROW
  begin
  select "QLBB"."SQ_AspNetUserClaims".nextval into :new."Id" from dual;
end;
/
--
-- IX_AspNetRoles_Name
--
  CREATE UNIQUE INDEX "IX_AspNetRoles_Name" ON "AspNetRoles" ("Name");
--
-- IX_AspNetUserClaims_UserId
--
  CREATE INDEX "IX_AspNetUserClaims_UserId" ON "AspNetUserClaims" ("UserId");
--
-- IX_AspNetUserLogins_UserId
--
  CREATE INDEX "IX_AspNetUserLogins_UserId" ON "AspNetUserLogins" ("UserId");
--
-- IX_AspNetUserRoles_RoleId
--
  CREATE INDEX "IX_AspNetUserRoles_RoleId" ON "AspNetUserRoles" ("RoleId");
--
-- IX_AspNetUserRoles_UserId
--
  CREATE INDEX "IX_AspNetUserRoles_UserId" ON "AspNetUserRoles" ("UserId");
--
-- IX_AspNetUsers_repr_1098102280
--
  CREATE INDEX "IX_AspNetUsers_repr_1098102280" ON "AspNetUsers" ("representImage_Id");
--
-- IX_AspNetUsers_UserName
--
  CREATE UNIQUE INDEX "IX_AspNetUsers_UserName" ON "AspNetUsers" ("UserName");
--
-- IX_Bakery_category_Id
--
  CREATE INDEX "IX_Bakery_category_Id" ON "Bakery" ("category_Id");
--
-- IX_Bakery_images_Id
--
  CREATE INDEX "IX_Bakery_images_Id" ON "Bakery" ("images_Id");
--
-- IX_Billdetails_Bakery_ID
--
  CREATE INDEX "IX_Billdetails_Bakery_ID" ON "Billdetails" ("Bakery_ID");
--
-- IX_Billdetails_Bill_Id
--
  CREATE INDEX "IX_Billdetails_Bill_Id" ON "Billdetails" ("Bill_Id");
--
-- IX_Introduction_Author_Id
--
  CREATE INDEX "IX_Introduction_Author_Id" ON "Introduction" ("Author_Id");
--
-- IX_Introduction_image_Id
--
  CREATE INDEX "IX_Introduction_image_Id" ON "Introduction" ("image_Id");
--
-- IX_Log_bakeryuser_Id
--
  CREATE INDEX "IX_Log_bakeryuser_Id" ON "Log" ("bakeryuser_Id");
--
-- IX_Slider_image_Id
--
  CREATE INDEX "IX_Slider_image_Id" ON "Slider" ("image_Id");
--
-- FK_AspNetUserClaims_UserId
--
ALTER TABLE "QLBB"."AspNetUserClaims" ADD CONSTRAINT "FK_AspNetUserClaims_UserId" FOREIGN KEY ("UserId") REFERENCES "QLBB"."AspNetUsers"("Id") ON DELETE CASCADE ENABLE;

--
-- FK_AspNetUserLogins_UserId
--
ALTER TABLE "QLBB"."AspNetUserLogins" ADD CONSTRAINT "FK_AspNetUserLogins_UserId" FOREIGN KEY ("UserId") REFERENCES "QLBB"."AspNetUsers"("Id") ON DELETE CASCADE ENABLE;

--
-- FK_AspNetUserRoles_RoleId
--
ALTER TABLE "QLBB"."AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_RoleId" FOREIGN KEY ("RoleId") REFERENCES "QLBB"."AspNetRoles"("Id") ON DELETE CASCADE ENABLE;

--
-- FK_AspNetUserRoles_UserId
--
ALTER TABLE "QLBB"."AspNetUserRoles" ADD CONSTRAINT "FK_AspNetUserRoles_UserId" FOREIGN KEY ("UserId") REFERENCES "QLBB"."AspNetUsers"("Id") ON DELETE CASCADE ENABLE;

--
-- FK_AspNetUsers_repre_889109962
--
ALTER TABLE "QLBB"."AspNetUsers" ADD CONSTRAINT "FK_AspNetUsers_repre_889109962" FOREIGN KEY ("representImage_Id") REFERENCES "QLBB"."Image"("Id") ENABLE;

--
-- FK_Bakery_category_Id
--
ALTER TABLE "QLBB"."Bakery" ADD CONSTRAINT "FK_Bakery_category_Id" FOREIGN KEY ("category_Id") REFERENCES "QLBB"."Category"("Id") ENABLE;

--
-- FK_Bakery_images_Id
--
ALTER TABLE "QLBB"."Bakery" ADD CONSTRAINT "FK_Bakery_images_Id" FOREIGN KEY ("images_Id") REFERENCES "QLBB"."Image"("Id") ENABLE;

--
-- FK_Billdetails_Bakery_ID
--
ALTER TABLE "QLBB"."Billdetails" ADD CONSTRAINT "FK_Billdetails_Bakery_ID" FOREIGN KEY ("Bakery_ID") REFERENCES "QLBB"."Bakery"("ID") ENABLE;

--
-- FK_Billdetails_Bill_Id
--
ALTER TABLE "QLBB"."Billdetails" ADD CONSTRAINT "FK_Billdetails_Bill_Id" FOREIGN KEY ("Bill_Id") REFERENCES "QLBB"."Bill"("Id") ENABLE;

--
-- FK_Introduction_Author_Id
--
ALTER TABLE "QLBB"."Introduction" ADD CONSTRAINT "FK_Introduction_Author_Id" FOREIGN KEY ("Author_Id") REFERENCES "QLBB"."AspNetUsers"("Id") ENABLE;

--
-- FK_Introduction_image_Id
--
ALTER TABLE "QLBB"."Introduction" ADD CONSTRAINT "FK_Introduction_image_Id" FOREIGN KEY ("image_Id") REFERENCES "QLBB"."Image"("Id") ENABLE;

--
-- FK_Log_bakeryuser_Id
--
ALTER TABLE "QLBB"."Log" ADD CONSTRAINT "FK_Log_bakeryuser_Id" FOREIGN KEY ("bakeryuser_Id") REFERENCES "QLBB"."AspNetUsers"("Id") ENABLE;

--
-- FK_Slider_image_Id
--
ALTER TABLE "QLBB"."Slider" ADD CONSTRAINT "FK_Slider_image_Id" FOREIGN KEY ("image_Id") REFERENCES "QLBB"."Image"("Id") ENABLE;

