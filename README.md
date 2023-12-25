# KirovTransportTax
Необходимо разработать WebApi для решения следующих задач:

## 1. Спроектировать структуру БД для хранения информации о марках, моделях, водителях и самих автомобилях.

   ![ER_scheme_kirov_transport_tax](https://github.com/elisaveta9/KirovTransportTax/assets/89607033/50890a90-ab77-4bf9-9513-f969589f1f15)
      *ER-схема БД*

   Изначально база данных должна содержать следующие значения:
   
   ![image](https://github.com/elisaveta9/KirovTransportTax/assets/89607033/c0d66a0a-8200-4bf3-8c75-3977ba9ed4f2)
      *Содержание табицы transport_type*
   
   ![image](https://github.com/elisaveta9/KirovTransportTax/assets/89607033/dcd7d254-7acc-4765-85af-4245da589cf8)
      *Содержание таблицы transport_tax_rate*

   Проверка данных проходит в базе данных. Код каждой таблицы:

   ```postgreSQL
   CREATE TABLE IF NOT EXISTS public.transport_type
(
    type text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT type_transport_pkey PRIMARY KEY (type)
)

CREATE TABLE IF NOT EXISTS public.transport_tax_rate
(
    id integer NOT NULL DEFAULT nextval('transport_tax_rate_id_seq'::regclass),
    transport_type text COLLATE pg_catalog."default" NOT NULL,
    min_horsepower integer NOT NULL,
    max_horsepower integer,
    tax_rate integer NOT NULL,
    CONSTRAINT transport_tax_rate_pkey PRIMARY KEY (id),
    CONSTRAINT transport FOREIGN KEY (transport_type)
        REFERENCES public.transport_type (type) MATCH FULL
        ON UPDATE CASCADE
        ON DELETE CASCADE,
    CONSTRAINT transport_tax_rate_check CHECK (max_horsepower = NULL::integer OR min_horsepower < max_horsepower)
)

CREATE TABLE IF NOT EXISTS public.driver
(
    passport text COLLATE pg_catalog."default" NOT NULL,
    last_name text COLLATE pg_catalog."default" NOT NULL,
    name text COLLATE pg_catalog."default" NOT NULL,
    patronymic text COLLATE pg_catalog."default",
    birthday date NOT NULL,
    CONSTRAINT driver_pkey PRIMARY KEY (passport),
    CONSTRAINT driver_birthday_check CHECK (EXTRACT(year FROM age(birthday::timestamp with time zone)) >= 18::numeric),
    CONSTRAINT driver_passport_check CHECK (passport ~* '\d{4} \d{6}'::text)
)

CREATE TABLE IF NOT EXISTS public.transport_brand
(
    brand text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT transport_brand_pkey PRIMARY KEY (brand)
)

CREATE TABLE IF NOT EXISTS public.transport_model
(
    model text COLLATE pg_catalog."default" NOT NULL,
    brand text COLLATE pg_catalog."default" NOT NULL,
    horsepower integer NOT NULL,
    release_year integer NOT NULL,
    transport_type text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT transport_model_pkey PRIMARY KEY (model),
    CONSTRAINT transport_model_brand_fkey FOREIGN KEY (brand)
        REFERENCES public.transport_brand (brand) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT transport_model_transport_type_fkey FOREIGN KEY (transport_type)
        REFERENCES public.transport_type (type) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT transport_model_release_year_check CHECK (release_year >= 1900 AND release_year::double precision <= date_part('year'::text, now())),
    CONSTRAINT transport_model_horsepower_check CHECK (horsepower > 0) NOT VALID
)

CREATE TABLE IF NOT EXISTS public.transport
(
    number_transport text COLLATE pg_catalog."default" NOT NULL,
    model text COLLATE pg_catalog."default" NOT NULL,
    registration_date date NOT NULL,
    driver text COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT transport_driver_pkey PRIMARY KEY (number_transport),
    CONSTRAINT transport_driver_fkey FOREIGN KEY (driver)
        REFERENCES public.driver (passport) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT transport_model_transport_fkey FOREIGN KEY (model)
        REFERENCES public.transport_model (model) MATCH SIMPLE
        ON UPDATE CASCADE
        ON DELETE CASCADE
        NOT VALID,
    CONSTRAINT transport_number_transport_check CHECK (number_transport ~* '^[АВЕКМНОРСТУХ][0-9]{3}[АВЕКМНОРСТУХ]{2}$'::text OR number_transport ~* '^[АВЕКМНОРСТУХ]{2}[0-9]{3,4}$'::text OR number_transport ~* '^[0-9]{4}[АВЕКМНОРСТУХ]{2}$'::text)
)
   ```

## 2. Разработать WebApi для получения/добавления/изменения информации о марках, моделях, водителях, автомобилях.

![Swagger-UI-Google-Chrome-2023-12-25-02-33-41 (2) (1)](https://github.com/elisaveta9/KirovTransportTax/assets/89607033/6c0cd77d-ecf9-433d-ba8c-54d946ce2319)
*Все действия, которые выполняет WebApi*


   
## 3. Разработать WebApi для получения отчета. Построение отчета в табличном виде: водитель, автомобиль, возраст авто, л.с., сумма налога, сортировка по сумме налога (по убыванию).
