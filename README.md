# cash-machine

# 2)

```
select   
	tbl."ReportsTo" as UserReported,
	count(tbl."ID"),	
	avg("Age")::numeric(2,0)	
from   
	"tblHierarquia" tbl 		  
where 
	 tbl."ReportsTo" is not null
group by 
	 tbl."ReportsTo"
Order By 
	 tbl."ReportsTo" asc
```
#3)a)

```
select ve."placa",
       cl."nome" 
from "Veiculo" ve
inner join "Cliente" cl on cl.cpf = ve."Cliente_cpf"

```

#3)b)

```
select pat."ender",
       est."dtEntrada",
       est."dtSaida"
from "Patio" pat 
inner join "Estaciona" est on est."Patio_num" = pat."num"
where est."Veiculo_placa" = 'BTG-2022'

```

#4) b

#5) b

#6) a

#7) d
