--
-- PostgreSQL database dump
--

\restrict hgs1CEdfbceefIKYEZRdm0dgZ9PooKswyilJBLUCgWcO83NT76JFf3AlNWlf8Bt

-- Dumped from database version 16.10
-- Dumped by pg_dump version 16.10

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: Angsurans; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Angsurans" (
    "Id" uuid NOT NULL,
    "KreditId" uuid NOT NULL,
    "Jumlah" numeric NOT NULL,
    "SudahDibayar" numeric NOT NULL,
    "JatuhTempo" timestamp with time zone NOT NULL,
    "Lunas" boolean NOT NULL,
    "PengajuanId" uuid NOT NULL,
    "JumlahAngsuran" numeric NOT NULL,
    "AngsuranKe" integer NOT NULL,
    "JumlahBayar" numeric,
    "TanggalBayar" timestamp with time zone,
    "StatusLunas" boolean NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


--
-- Name: Kredits; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Kredits" (
    "Id" uuid NOT NULL,
    "Plafon" numeric NOT NULL,
    "Bunga" numeric NOT NULL,
    "Tenor" integer NOT NULL,
    "Angsuran" numeric NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL,
    "UpdatedAt" timestamp with time zone NOT NULL
);


--
-- Name: PengajuanKredits; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."PengajuanKredits" (
    "Id" uuid NOT NULL,
    "Plafon" numeric NOT NULL,
    "Bunga" numeric NOT NULL,
    "Tenor" integer NOT NULL,
    "AngsuranPerBulan" numeric NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL
);


--
-- Name: Users; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Username" text,
    "Password" text,
    "PasswordHash" text,
    "CreatedAt" timestamp with time zone NOT NULL
);


--
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: -
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


--
-- Data for Name: Angsurans; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."Angsurans" ("Id", "KreditId", "Jumlah", "SudahDibayar", "JatuhTempo", "Lunas", "PengajuanId", "JumlahAngsuran", "AngsuranKe", "JumlahBayar", "TanggalBayar", "StatusLunas", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: Kredits; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."Kredits" ("Id", "Plafon", "Bunga", "Tenor", "Angsuran", "CreatedAt", "UpdatedAt") FROM stdin;
6d553a0f-2b8e-4ec0-a380-abb881e03a91	100000000	12	60	2140000	2025-08-16 18:44:58.279737+07	2025-08-16 18:44:58.279737+07
81919699-8b1b-4f54-b1f0-b7f32c552d01	50000000	10	36	1700000	2025-08-16 18:44:58.280218+07	2025-08-16 18:44:58.280218+07
2c4424f5-3f09-4b25-ac4a-3e2439802289	5	100	10	0.76	2025-08-16 21:28:54.743828+07	2025-08-16 21:28:54.743929+07
22e6ae5f-23b4-4337-8b3c-525d4dc9892f	5000000	10	15	355985.75	2025-08-16 21:29:26.464354+07	2025-08-16 21:29:26.464354+07
\.


--
-- Data for Name: PengajuanKredits; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."PengajuanKredits" ("Id", "Plafon", "Bunga", "Tenor", "AngsuranPerBulan", "CreatedAt") FROM stdin;
\.


--
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."Users" ("Id", "Username", "Password", "PasswordHash", "CreatedAt") FROM stdin;
417cd318-a0ca-4090-958a-a52e1ea3dc0b	admin	$2a$11$dEPfkx/o4xA9Ly3r9LIw2ufYxbBuEOBnmyy/1wTVInHBlKygGMnaG	\N	2025-08-16 18:44:56.796884+07
\.


--
-- Data for Name: __EFMigrationsHistory; Type: TABLE DATA; Schema: public; Owner: -
--

COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
20250816114340_Init	9.0.8
\.


--
-- Name: Angsurans PK_Angsurans; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Angsurans"
    ADD CONSTRAINT "PK_Angsurans" PRIMARY KEY ("Id");


--
-- Name: Kredits PK_Kredits; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Kredits"
    ADD CONSTRAINT "PK_Kredits" PRIMARY KEY ("Id");


--
-- Name: PengajuanKredits PK_PengajuanKredits; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."PengajuanKredits"
    ADD CONSTRAINT "PK_PengajuanKredits" PRIMARY KEY ("Id");


--
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- Name: IX_Angsurans_KreditId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_Angsurans_KreditId" ON public."Angsurans" USING btree ("KreditId");


--
-- Name: IX_Angsurans_PengajuanId; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_Angsurans_PengajuanId" ON public."Angsurans" USING btree ("PengajuanId");


--
-- Name: IX_Kredits_Plafon; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_Kredits_Plafon" ON public."Kredits" USING btree ("Plafon");


--
-- Name: IX_Kredits_Tenor; Type: INDEX; Schema: public; Owner: -
--

CREATE INDEX "IX_Kredits_Tenor" ON public."Kredits" USING btree ("Tenor");


--
-- Name: Angsurans FK_Angsurans_Kredits_KreditId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Angsurans"
    ADD CONSTRAINT "FK_Angsurans_Kredits_KreditId" FOREIGN KEY ("KreditId") REFERENCES public."Kredits"("Id") ON DELETE CASCADE;


--
-- Name: Angsurans FK_Angsurans_PengajuanKredits_PengajuanId; Type: FK CONSTRAINT; Schema: public; Owner: -
--

ALTER TABLE ONLY public."Angsurans"
    ADD CONSTRAINT "FK_Angsurans_PengajuanKredits_PengajuanId" FOREIGN KEY ("PengajuanId") REFERENCES public."PengajuanKredits"("Id") ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

\unrestrict hgs1CEdfbceefIKYEZRdm0dgZ9PooKswyilJBLUCgWcO83NT76JFf3AlNWlf8Bt

