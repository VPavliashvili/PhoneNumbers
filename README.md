### Run
მონაცემთა ბაზის მარტივად გაზიარებისთვის [დოქერის იმიჯი](https://hub.docker.com/repository/docker/vpavliashvili/my-postgres-image) ავტვირთე ჰაბზე
`docker run --name my-postgres -p 7878:5432 -d vpavliashvili/my-postgres-image`
პორტი 7878 მაქვს აპსეთინგებში მითითებული, სხვა მხრივ აზრი არ აქვს რაიქნება.
ბაზის სახელია phone_numbers, იუზერი vpavliashvili, პაროლი test.
ბაზის სქემა მზად არის, ცხრილებიც მინიმალურად შევსებულია. users ცხრილში ყველა იუზერის პაროლია ასევე tests

### Used Stack
- .Net6/C# 10
- Serilog
- Dapper
- PostgreSQL
- JWT Authentication
 
