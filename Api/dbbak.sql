--
-- PostgreSQL database dump
--

-- Dumped from database version 15.1 (Debian 15.1-1.pgdg110+1)
-- Dumped by pg_dump version 15.1

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
-- Name: contact_numbers; Type: TABLE; Schema: public; Owner: vpavliashvili
--

CREATE TABLE public.contact_numbers (
    id integer NOT NULL,
    contact_id integer NOT NULL,
    number character varying NOT NULL
);


ALTER TABLE public.contact_numbers OWNER TO vpavliashvili;

--
-- Name: contact_numbers_id_seq; Type: SEQUENCE; Schema: public; Owner: vpavliashvili
--

CREATE SEQUENCE public.contact_numbers_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.contact_numbers_id_seq OWNER TO vpavliashvili;

--
-- Name: contact_numbers_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vpavliashvili
--

ALTER SEQUENCE public.contact_numbers_id_seq OWNED BY public.contact_numbers.id;


--
-- Name: contacts; Type: TABLE; Schema: public; Owner: vpavliashvili
--

CREATE TABLE public.contacts (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL
);


ALTER TABLE public.contacts OWNER TO vpavliashvili;

--
-- Name: user_contacts_id_seq; Type: SEQUENCE; Schema: public; Owner: vpavliashvili
--

CREATE SEQUENCE public.user_contacts_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.user_contacts_id_seq OWNER TO vpavliashvili;

--
-- Name: user_contacts_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vpavliashvili
--

ALTER SEQUENCE public.user_contacts_id_seq OWNED BY public.contacts.id;


--
-- Name: users; Type: TABLE; Schema: public; Owner: vpavliashvili
--

CREATE TABLE public.users (
    id integer NOT NULL,
    name character varying(50) NOT NULL,
    surname character varying(50) NOT NULL,
    username character varying(50) NOT NULL,
    password character varying(50) NOT NULL,
    salt character varying(50) NOT NULL
);


ALTER TABLE public.users OWNER TO vpavliashvili;

--
-- Name: users_id_seq; Type: SEQUENCE; Schema: public; Owner: vpavliashvili
--

CREATE SEQUENCE public.users_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.users_id_seq OWNER TO vpavliashvili;

--
-- Name: users_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: vpavliashvili
--

ALTER SEQUENCE public.users_id_seq OWNED BY public.users.id;


--
-- Name: contact_numbers id; Type: DEFAULT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.contact_numbers ALTER COLUMN id SET DEFAULT nextval('public.contact_numbers_id_seq'::regclass);


--
-- Name: contacts id; Type: DEFAULT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.contacts ALTER COLUMN id SET DEFAULT nextval('public.user_contacts_id_seq'::regclass);


--
-- Name: users id; Type: DEFAULT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.users ALTER COLUMN id SET DEFAULT nextval('public.users_id_seq'::regclass);


--
-- Data for Name: contact_numbers; Type: TABLE DATA; Schema: public; Owner: vpavliashvili
--

COPY public.contact_numbers (id, contact_id, number) FROM stdin;
22	17	123456789
26	19	12345dasdas6789
27	19	123321dasd444
\.


--
-- Data for Name: contacts; Type: TABLE DATA; Schema: public; Owner: vpavliashvili
--

COPY public.contacts (id, name, surname) FROM stdin;
17	smasdasdth	srasdasdn
19	string	string
\.


--
-- Data for Name: users; Type: TABLE DATA; Schema: public; Owner: vpavliashvili
--

COPY public.users (id, name, surname, username, password, salt) FROM stdin;
3	string	string	usr1	9E0622F2E2F6AD1E35868A877BB35F09	���t ��q��������^���)\v�\v�]~y\\
4	asdas	sdas	qwe	B84DEA828772E3E805FCBFB47A4F3657	#{p���2��eKex�M�!0�_:��Ӓ��
\.


--
-- Name: contact_numbers_id_seq; Type: SEQUENCE SET; Schema: public; Owner: vpavliashvili
--

SELECT pg_catalog.setval('public.contact_numbers_id_seq', 35, true);


--
-- Name: user_contacts_id_seq; Type: SEQUENCE SET; Schema: public; Owner: vpavliashvili
--

SELECT pg_catalog.setval('public.user_contacts_id_seq', 21, true);


--
-- Name: users_id_seq; Type: SEQUENCE SET; Schema: public; Owner: vpavliashvili
--

SELECT pg_catalog.setval('public.users_id_seq', 4, true);


--
-- Name: contact_numbers contact_numbers_pk; Type: CONSTRAINT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.contact_numbers
    ADD CONSTRAINT contact_numbers_pk PRIMARY KEY (id);


--
-- Name: contacts user_contacts_pk; Type: CONSTRAINT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.contacts
    ADD CONSTRAINT user_contacts_pk PRIMARY KEY (id);


--
-- Name: users users_pkey; Type: CONSTRAINT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_pkey PRIMARY KEY (id);


--
-- Name: users users_username_key; Type: CONSTRAINT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.users
    ADD CONSTRAINT users_username_key UNIQUE (username);


--
-- Name: user_contacts_id_idx; Type: INDEX; Schema: public; Owner: vpavliashvili
--

CREATE UNIQUE INDEX user_contacts_id_idx ON public.contacts USING btree (id);


--
-- Name: contact_numbers contact_numbers_fk; Type: FK CONSTRAINT; Schema: public; Owner: vpavliashvili
--

ALTER TABLE ONLY public.contact_numbers
    ADD CONSTRAINT contact_numbers_fk FOREIGN KEY (contact_id) REFERENCES public.contacts(id) ON UPDATE CASCADE ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

