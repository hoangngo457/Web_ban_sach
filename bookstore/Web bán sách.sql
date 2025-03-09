use master
if exists (select * from sysdatabases where name = 'Book_Store')
	drop database Book_Store
go
create database Book_Store
go
use Book_Store
go


CREATE TABLE Category
(
  IdCate INT IDENTITY(1,1),
  name NVARCHAR(100) ,
  PRIMARY KEY (IdCate)
);

CREATE TABLE Author
(
  IdAu INT IDENTITY(1,1),
  name NVARCHAR(50) ,
  phone CHAR(10) ,
  address NVARCHAR(50) ,
  story NVARCHAR(max) ,
  PRIMARY KEY (IdAu)
);
CREATE TABLE Users
(
  IdUser INT IDENTITY(1,1),
  username VARCHAR(20) ,
  password VARCHAR(max) ,
  confirmPassword VARCHAR(max) ,
  role NVARCHAR(20) ,
  name NVARCHAR(50) ,
  address NVARCHAR(50) ,
  email VARCHAR(50) ,
  phone CHAR(10) ,
  PRIMARY KEY (IdUser),
);

CREATE TABLE Orders
(
  IdOrder INT IDENTITY(1,1),
  status NVARCHAR(50) ,
  oderDate DATE ,
  MaKH INT ,
  PRIMARY KEY (IdOrder),
  FOREIGN KEY (MaKH) REFERENCES Users(IdUser)
);



CREATE TABLE Publisher--Nhà xuất bản
(
  IdPub INT IDENTITY(1,1),
  name NVARCHAR(50) ,
  phone CHAR(10) ,
  address NVARCHAR(50) ,
  PRIMARY KEY (IdPub)
);

CREATE TABLE Book
(
  IdBook INT IDENTITY(1,1),
  nameBook NVARCHAR(500) ,
  description NVARCHAR(max) ,
  price INT ,
  image VARCHAR(50) ,
  quality INT ,
  IdCate INT ,
  IdPub INT ,
  PRIMARY KEY (IdBook),
  FOREIGN KEY (IdCate) REFERENCES Category(IdCate),
  FOREIGN KEY (IdPub) REFERENCES Publisher(IdPub)
);

CREATE TABLE Participate-- Tác giả tham gia viết sách
(
  IdAu INT ,
  IdBook INT ,
  role NVARCHAR(50) ,
  location NVARCHAR(50) ,
  PRIMARY KEY (IdAu, IdBook),
  FOREIGN KEY (IdAu) REFERENCES Author(IdAu),
  FOREIGN KEY (IdBook) REFERENCES Book(IdBook)
);

CREATE TABLE OrderDetail
(
  IdBook INT ,
  IdOrder INT ,
  quanlity INT ,
  price INT ,
  PRIMARY KEY (IdBook, IdOrder),
  FOREIGN KEY (IdBook) REFERENCES Book(IdBook),
  FOREIGN KEY (IdOrder) REFERENCES Orders(IdOrder)
);


select *  from users



--INSERT Category
INSERT INTO Category(name)
values(N'Sách giáo khoa'),
(N'Sách Ngoại ngữ'),
(N'kinh tế'),
(N'Light Novel');
--INSERT Publisher
INSERT INTO Publisher(name, phone, address)
values(N'NXB Kim Đồng','0356734323','34 a'),
(N'NXB Kim Đồng','0356734323','34 a'),
(N'NXB Trẻ','0356734323','34 a'),
(N'NXB Nhã Nam','0356734323','34 a'),
(N'NXB Đinh Tị','0356734323','34 a'),
(N'NXB Alphabooks','0356734323','34 a'),
(N'NXB Phương Nam','0356734323','34 a');



--Thêm admin
INSERT INTO Users(username, password, confirmPassword, role)
values('admin','admin', 'admin','Chủ web')
--INSERT Book
INSERT INTO Book(nameBook, description, price, image, quality, IdCate, IdPub)
values(N'Bộ Sách Giáo Dục Toàn Diện Cho Học Sinh Tiểu Học( Bộ 3 Cuốn)',N'Trong quá trình trưởng thành, mỗi một trải nghiệm đều là một thu hoạch, thử nghĩ xem, trong những trải nghiệm này, chúng ta đã làm việc gì đáng được mọi người khen ngợi, hoặc việc gì cần phải kiểm điểm lại. Học cách tư duy tích cực và thực hành thực tế, quá trình rèn luyện từ suy nghĩ tới hành động, trẻ sẽ dần dần xây dựng được sức mạnh của mình.','180000','sachgiaoductoandien.png','10','1','1'),
(N'Bộ Sách Giáo Khoa Lớp 4',N'Trọn bộ gồm các cuốn:
- Tiếng Việt 4 (Tập 1)
- Tiếng Việt 4 (Tập 2)
- Toán 4
- Khoa học 4
- Lịch sử và địa lí 4
- Âm nhạc 4
- Mĩ thuật 4
- Kĩ thuật 4
- Đạo đức 4','80000','sgkl4.png','12','1','1'),
(N'Bộ Sách Bài Học Lớp 10 (Trọn Bộ 14 Cuốn) (Ban Chuẩn)',N'Bộ Sách Bài Học Lớp 10, trọn bộ gồm 14 cuốn như sau:
01. Công nghệ Lớp 10 - Giá bìa: 13.800 VNĐ
02. Giáo dục công dân Lớp 10 - Giá bìa: 5.900 VNĐ
03. Tin học Lớp 10 - Giá bìa: 8.500 VNĐ
04. Giáo dục quốc phòng an ninh Lớp 10 - Giá bìa: 8.800 VNĐ
05. Đại số Lớp 10 - Giá bìa: 8.400 VNĐ
06. Hình học Lớp 10 - Giá bìa: 5.500 VNĐ
07. Vật lí Lớp 10 - Giá bìa: 13.400 VNĐ
08. Hóa học Lớp 10 - Giá bìa: 12.800 VNĐ
09. Sinh học Lớp 10 - Giá bìa: 10.000 VNĐ
10. Ngữ văn Lớp 10 (Tập Một) - Giá bìa: 8.500 VNĐ
11. Ngữ văn Lớp 10 (Tập Hai) - Giá bìa: 7.800 VNĐ
12. Lịch sử Lớp 10 - Giá bìa: 12.000 VNĐ
13. Địa lí Lớp 10 - Giá bìa: 12.300 VNĐ
14. Tiếng Anh Lớp 10 - Giá bìa: 13.700 VNĐ','141400','bosgk10.png','15','1','2'),
(N'YBM TOEIC Listening 1000 - Vol 2 (Tái Bản 2023)',N'BM TOEIC Listening 1000 là bộ sách giúp người học tự ôn luyện cho phần thi Nghe của bài thi TOEIC. Trong đó, Vol. 1 được thiết kế dành cho người học nhắm tới mục tiêu đạt 500+ điểm TOEIC và Vol. 2 dành cho người học có mục tiêu đạt 700+. (Mức 450 điểm là yêu cầu đối với sinh viên tốt nghiệp cao đẳng; mức 650 điểm là yêu cầu chung đối với sinh viên tốt nghiệp đại học hệ đào tạo 4-5 năm, nhân viên, trưởng nhóm tại các doanh nghiệp có yếu tố nước ngoài).','220000','YBMToeic.png','20','2','3'),
(N'Tiếng Nhật Công Nghệ Thông Tin: Hội Thoại Trong Dự Án Phần Mềm',N'Hiện nay, mối quan hệ giữa Việt Nam và Nhật Bản đang phát triển nhanh chóng về mọi mặt, đặc biệt là trên lĩnh vực công nghệ thông tin. Cùng với đó, nhu cầu nhân lực biết tiếng Nhật cũng đang rất cấp bách. Đáp ứng nhu cầu đó, tiếp nối sự ra đời của cuốn sách “Tiếng Nhật công nghệ thông tin trong ngành phần mềm”, nhóm tác giả đã biên soạn cuốn “Tiếng Nhật công nghệ thông tin: Hội thoại trong dự án phần mềm” này dành cho đối tượng có trình độ tiếng Nhật tương đương N3 với các chủ đề phong phú, dễ nhớ và dễ áp dụng vào dự án thực tế.','246500','hoithoaitrongduanpm.png','15','2','4'),
(N'Hoàn Thiện Kỹ Năng Dựng Câu Tiếng Anh - Refining English Sentence Building',N'Nhan đề cuốn sách đã thể hiện rõ mong muốn của tác giả: đem đến cho người học tiếng Anh số lượng hữu hạn các mẫu câu nhưng lại có thể giúp tạo ra số lượng vô hạn các câu. Đây là mục tiêu của nhiều cuốn sách tương tự, nhưng cuốn sách này có nhiều ưu điểm vượt trội dể có thể biến mong muốn của tác giả và của người học thành hiện thực. Cuốn sách này thực sự đóng vai không chỉ là một “chuyên gia” cung cấp kiến thức và kỹ năng cần thiết về đơn vị câu, mà còn là một “người bạn” giúp người học nắm kiến thức và kỹ năng đó một cách thoải mái, nhẹ nhàng và chắc chắn. Tác giả cũng hy vọng đây sẽ là cuốn sách “gối đầu giường” cho người học tiếng Anh ở mọi trình độ, mọi giai đoạn và mọi lứa tuổi.','136000','hoanthiendungcau.png','34','2','5');

INSERT INTO Orders (oderDate, MaKH)
VALUES 
('2023-01-01', 2),
('2023-01-02', 5),
('2023-01-04', 4),
('2023-01-05', 5),
('2023-01-06', 6),
('2023-01-09', 3),
('2023-01-11', 4),
('2023-01-14', 4),
('2023-01-16', 6),
('2023-01-19', 6),
('2023-01-20', 2),
('2023-02-01', 4), ('2023-02-02', 5), ('2023-02-03', 3), ('2023-02-04', 4), ('2023-02-05', 5),
('2023-02-06', 6), ('2023-02-07', 4), ('2023-02-08', 5), ('2023-02-09', 6), ('2023-02-10', 4),
('2023-03-01', 5), ('2023-03-02', 6), ('2023-03-03', 4), ('2023-03-04', 5), ('2023-03-05', 6),
('2023-03-06', 4), ('2023-03-07', 5), ('2023-03-08', 6), ('2023-03-09', 4), ('2023-03-10', 5);

INSERT INTO OrderDetail (IdBook, IdOrder, quanlity, Price)
VALUES 
(5, 3, 2, 100200),
(12, 4, 3, 100300),
(18, 5, 1, 100400),
(8, 6, 3, 100500),
(25, 7, 2, 100600),
(10, 8, 1, 100700),
(30, 9, 3, 100800),
(6, 10, 2, 100900),
(15, 11, 1, 100100),
(3, 3, 3, 100000),
(5, 12, 2, 150000),
(12, 13, 3, 151000),
(18, 14, 1, 152000),
(8, 15, 3, 153000),
(25, 16, 2, 154000),
(10, 17, 1, 155000),
(30, 18, 3, 156000),
(6, 19, 2, 157000),
(15, 20, 1, 158000),
(3, 21, 3, 159000),
(5, 22, 2, 160000),
(12, 23, 3, 161000),
(18, 24, 1, 162000),
(8, 25, 3, 163000),
(25, 26, 2, 164000),
(10, 27, 1, 165000),
(30, 28, 3, 166000),
(6, 29, 2, 167000),
(15, 30, 1, 168000),
(3, 31, 3, 169000);

