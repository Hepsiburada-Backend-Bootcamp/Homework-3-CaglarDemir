# HomeWork2-CaglarDemir
Hepsiburada Backend Bootcamp 3.Hafta Ödevi

### **Daha tamamlanmadı. Tamamlanınca bu dosya güncellenecektir**

Temel CRUD işlemleri ile ilaç kaydı yapılabilen bir resful Web-API projesi. **Swagger** desteği eklenmiştir. Database olarak EntityFramework ile oluşturulmuş bir database kullanılmıştır. Company nesnesi için hem **EntityFramework** hem **Dapper** repositorysi yazılmıştır. Sort ve list özellikleri için `System.Linq.Dynamic.Core` paketinin sorgularda `string` kullanabilme işlevi kullanılarak extension oluşturulmuştur. Dto mappinglerinde **Mapster** kullanılmıştır. **Serilog** ile konsola, `.txt` ve `.json` uzantılı dosyalara loglama yapmaktadır. DockerHub'a publish edilmiştir ancak içerisinde sadece uygulama mevcuttur, database dahil değildir. https://hub.docker.com/r/cagdem/pharmacyapi

Database view:

![image](https://user-images.githubusercontent.com/15106912/135694964-82a077bc-bde8-4c0e-adf2-8a86a348ab0c.png)

Konsol log örneği:

![Konsol log örneği](https://user-images.githubusercontent.com/15106912/135689741-e7b4639b-1295-49ed-8a13-23f390f13d27.png)

Swagger:

![Swagger](https://user-images.githubusercontent.com/15106912/135689762-c57766bc-357f-4323-9e6c-a5148dcf68da.png)


## /api/v1/firmalar
Get ile kullanıldığında bütün firmaları getirir.

Post ile kullanıldığında body içerisindeki firmayı ekler.

## /api/v1/firmalar/{id}
Get ile kullanıldığında routerdan gelen `id`'ye sahip firmayı getirir.

Delete ile kullanıldığında routerdan gelen `id`'ye sahip firma silinir.

Put ile kullanıldığında routerdan gelen `id`'ye sahip firmayı güncellemek için body içerisindeki firmayı kullanır.


## /api/v1/ilaclar
Post ile body içerisinde gönderilen ilacı ekler.
## /api/v1/ilaclar/{id}
Get ile kullanıldığında routerdan gelen `id`'ye sahip ilacı getirir.

Put ile kullanıldığında routerdan gelen `id`'ye sahip ilacı güncellemek için body içerisindeki ilacı kullanır.

Delete ile kullanıldığında routerdan gelen `id`'ye sahip ilac silinir.
## /api/v1/ilaclar/List
Get ile kullanıldığında bütün ilaçları databasedeki sıralamaya göre listeler.
## /api/v1/ilaclar/List?list={FieldAdi}="{FieldDeğeri}"
Get ile kullanıldığında istenilen alanda istenilen değer sorgulanabilir. Örneğin `api/v1/ilaclar/List?list=Company="Y"` yapıldığında Y firmasının ilaçları listelenir.

## /api/v1/ilaclar/Sort?sort={FieldAdi}
Get ile kullanıldığında verilen alana göre sıralanır. Örneğin `/api/v1/ilaclar/Sort?sort=name` yapıldığında bütün veriler isme göre alfabetik sıralanır.

### To-Do
- Http dönüş kodları düzenlenmeli. Dönüşlerin detaylandırılabilmesi için özel bir result nesnesi düzenlenebilir.
- Order ve OrderDetail nesneleri için servis ve controller yazılmalı.

