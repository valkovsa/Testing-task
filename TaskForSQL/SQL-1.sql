-- Найти все полностью оплаченные заказы. Заказы оплачиваются в порядке очередности по мере поступления заказа.
declare @customers table (id int, name nvarchar(20))
declare @orders table (id int, summa numeric(18,2), customerId int)
declare @payments table (customerId int, payment numeric(18,2))

insert @customers (id, name)
values 
	(1, N'Первый'), 
	(2, N'Второй'), 
	(3, N'Третий'),
	(4, N'Четвертый')
	
insert @orders (id, summa, customerId)
values 
	(1, 10, 1), 
	(2, 15, 1), 
	(3, 20, 1), 
	(4, 25, 1), 
	(5, 12, 2), 
	(6, 14, 2), 
	(7, 200, 2), 
	(8, 100, 3), 
	(9, 200, 3)
insert @payments (customerId, payment)
values 
	(1, 30), 
	(2, 500), 
	(3, 100), 
	(4, 20)



-- -- SOLUTION -- --

select payments.customerId,
	   customers.name,
	   orders.id as orderId,
	   orders.summa as orderSum,
	   payments.payment as customerPaid
from @payments as payments
join @customers as customers on customers.id = payments.customerId
join (
	select *,
		   SUM(o.summa) over(Partition by o.customerid ORDER BY o.id) as cumulativeTotal
	from @orders as o) as orders on orders.customerId = payments.customerId
where payments.payment >= orders.cumulativeTotal


