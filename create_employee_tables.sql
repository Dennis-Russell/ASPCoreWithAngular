drop sequence employee_seq;
 
drop table tblEmployee;


CREATE SEQUENCE employee_seq
  MINVALUE 1
  MAXVALUE 999999999999999999999999999
  START WITH 1
  INCREMENT BY 1
  CACHE 20;

/

Create table tblEmployee(      
    EmployeeId int  NOT NULL,      
    Name varchar2(20) NOT NULL,      
    City varchar2(20) NOT NULL,      
    Department varchar2(20) NOT NULL,      
    Gender varchar2(6) NOT NULL        
);

/


Create or replace procedure spAddEmployee         
(        
    Name        IN  VARCHAR2,         
    City        IN  VARCHAR2,        
    Department  IN  VARCHAR2,        
    Gender      IN  VARCHAR2        
)        
as         
Begin         
    Insert into tblEmployee (employeeId,Name,City,Department, Gender)         
    Values (employee_seq.nextval,Name,City,Department, Gender);
    commit;         
exception
    when others then
        raise;
End;
/

Create or replace procedure spUpdateEmployee        
(        
   EmpId        IN  INTEGER ,      
   Name         IN  VARCHAR2,       
   City         IN  VARCHAR2,      
   Department   IN  VARCHAR2,      
   Gender       IN VARCHAR2      
)        
as        
begin        
   Update tblEmployee         
   set Name=Name,        
   City=City,        
   Department=Department,      
   Gender=Gender        
   where EmployeeId=EmpId;
    commit;         
exception
    when others then
        raise;
End;
/

Create or replace procedure spDeleteEmployee       
(        
   EmpId    IN  int        
)        
as         
begin        
   Delete from tblEmployee where EmployeeId=EmpId;        
    commit;         
exception
    when others then
        raise;
End;
/

Create or replace procedure spGetAllEmployees ( p_cursor OUT SYS_REFCURSOR)  
as      
Begin
    OPEN p_cursor FOR
    select *      
    from tblEmployee   
    order by EmployeeId;      
exception
    when others then
        raise;
End;
/
