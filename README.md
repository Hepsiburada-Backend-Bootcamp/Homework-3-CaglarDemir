# HomeWork3-CaglarDemir
Hepsiburada Backend Bootcamp 3.Hafta Ödevi

Temel CRUD işlemleri ile ilaç kaydı yapılabilen bir resful Web-API projesi. **Swagger** desteği eklenmiştir. Database olarak EntityFramework ile oluşturulmuş bir database kullanılmıştır. Company nesnesi için hem **EntityFramework** hem **Dapper** repositorysi yazılmıştır. Sort ve list özellikleri için `System.Linq.Dynamic.Core` paketinin sorgularda `string` kullanabilme işlevi kullanılarak extension oluşturulmuştur. Dto mappinglerinde **Mapster** kullanılmıştır. **Serilog** ile konsola, `.txt` ve `.json` uzantılı dosyalara loglama yapmaktadır. DockerHub'a publish edilmiştir ancak içerisinde sadece uygulama mevcuttur, database dahil değildir. https://hub.docker.com/r/cagdem/pharmacyapi

Servislerin ve Controller'ların unit testleri **Xunit** ve **Moq** kullanılarak yapıldı. Sonuçlara göre bazı refactoringler yapıldı. Tamamlanması gereken refactoringler mevcut. Integration test için **in-memory** database kullanıldı. Code coverage için **Coverlet** ve **Fine Code Coverage** toolları kullanıldı.

Yapılan testler:

![image](https://user-images.githubusercontent.com/15106912/136807303-84cfc679-62fd-4dbc-a699-65e8f27a2f67.png)

Code Coverage Özeti:

![image](https://user-images.githubusercontent.com/15106912/136807432-71ca07a7-3ca4-42f3-810b-3495a4e751c0.png)

Code Coverage:

![image](https://user-images.githubusercontent.com/15106912/136807365-28c5895b-389c-479d-a67c-33971ed91332.png)

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
Get ile kullanıldığında istenilen alanda istenilen değer sorgulanabilir. Örneğin `api/v1/ilaclar/List?list=CompanyName="Y"` yapıldığında Y firmasının ilaçları listelenir.

## /api/v1/ilaclar/Sort?sort={FieldAdi}
Get ile kullanıldığında verilen alana göre sıralanır. Örneğin `/api/v1/ilaclar/Sort?sort=name` yapıldığında bütün veriler isme göre alfabetik sıralanır.

### To-Do
- Http dönüş kodları düzenlenmeli. Dönüşlerin detaylandırılabilmesi için özel bir result nesnesi düzenlenebilir.
- Order ve OrderDetail nesneleri için servis ve controller yazılmalı.

