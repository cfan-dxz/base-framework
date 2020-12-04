FROM mcr.microsoft.com/dotnet/core/aspnet:3.1

WORKDIR /app

COPY ./publish .

RUN /bin/cp /usr/share/zoneinfo/Asia/Shanghai /etc/localtime && echo 'Asia/Shanghai' >/etc/timezone

RUN sed -i 's/TLSv1.2/TLSv1.0/g' /etc/ssl/openssl.cnf

ENTRYPOINT ["dotnet", "Demo.Idp.Web.dll"]
