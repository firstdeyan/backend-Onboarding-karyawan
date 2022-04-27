PGDMP                         z            lms    14.2    14.2 ;    A           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            B           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            C           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            D           1262    16525    lms    DATABASE     c   CREATE DATABASE lms WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_Indonesia.1252';
    DROP DATABASE lms;
                postgres    false            �            1259    16526    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            �            1259    33000 
   activities    TABLE     �   CREATE TABLE public.activities (
    id integer NOT NULL,
    activity_name text NOT NULL,
    activity_description text NOT NULL,
    category_id integer NOT NULL
);
    DROP TABLE public.activities;
       public         heap    postgres    false            �            1259    32999    activities_id_seq    SEQUENCE     �   ALTER TABLE public.activities ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activities_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    218            �            1259    33063    activities_owned    TABLE     9  CREATE TABLE public.activities_owned (
    id integer NOT NULL,
    user_email text NOT NULL,
    activities_id integer NOT NULL,
    start_date text NOT NULL,
    end_date text NOT NULL,
    status text NOT NULL,
    validated boolean NOT NULL,
    mentor_email text NOT NULL,
    activity_note text NOT NULL
);
 $   DROP TABLE public.activities_owned;
       public         heap    postgres    false            �            1259    33062    activities_owned_id_seq    SEQUENCE     �   ALTER TABLE public.activities_owned ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activities_owned_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    222            �            1259    33014    activity_details    TABLE       CREATE TABLE public.activity_details (
    id integer NOT NULL,
    activity_id integer NOT NULL,
    detail_name text NOT NULL,
    detail_desc text NOT NULL,
    detail_link text NOT NULL,
    detail_type text NOT NULL,
    detail_urutan integer NOT NULL
);
 $   DROP TABLE public.activity_details;
       public         heap    postgres    false            �            1259    33013    activity_details_id_seq    SEQUENCE     �   ALTER TABLE public.activity_details ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activity_details_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    220            �            1259    33112    admin    TABLE        CREATE TABLE public.admin (
    email text NOT NULL,
    admin_name text NOT NULL,
    "passwordHash" bytea,
    "passwordSalt" bytea,
    role_id integer NOT NULL,
    jobtitle_id integer NOT NULL,
    gender text NOT NULL,
    birthdate text NOT NULL,
    phone_number text NOT NULL
);
    DROP TABLE public.admin;
       public         heap    postgres    false            �            1259    32976 
   categories    TABLE     �   CREATE TABLE public.categories (
    id integer NOT NULL,
    category_name text NOT NULL,
    category_description text NOT NULL,
    duration integer NOT NULL
);
    DROP TABLE public.categories;
       public         heap    postgres    false            �            1259    32975    categories_id_seq    SEQUENCE     �   ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    216            �            1259    24773 
   job_titles    TABLE     �   CREATE TABLE public.job_titles (
    id integer NOT NULL,
    jobtitle_name text NOT NULL,
    jobtitle_description text NOT NULL
);
    DROP TABLE public.job_titles;
       public         heap    postgres    false            �            1259    24772    job_titles_id_seq    SEQUENCE     �   ALTER TABLE public.job_titles ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.job_titles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    213            �            1259    24752    roles    TABLE     �   CREATE TABLE public.roles (
    id integer NOT NULL,
    role_name text NOT NULL,
    role_description text NOT NULL,
    role_platform text NOT NULL
);
    DROP TABLE public.roles;
       public         heap    postgres    false            �            1259    24751    roles_id_seq    SEQUENCE     �   ALTER TABLE public.roles ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.roles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    211            �            1259    24785    user    TABLE     W  CREATE TABLE public."user" (
    email text NOT NULL,
    name text NOT NULL,
    "passwordHash" bytea,
    "passwordSalt" bytea,
    role_id integer DEFAULT 0 NOT NULL,
    jobtitle_id integer DEFAULT 0 NOT NULL,
    gender text NOT NULL,
    birthdate text NOT NULL,
    phone_number text NOT NULL,
    progress double precision NOT NULL
);
    DROP TABLE public."user";
       public         heap    postgres    false            0          0    16526    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    209   aI       9          0    33000 
   activities 
   TABLE DATA           Z   COPY public.activities (id, activity_name, activity_description, category_id) FROM stdin;
    public          postgres    false    218   �J       =          0    33063    activities_owned 
   TABLE DATA           �   COPY public.activities_owned (id, user_email, activities_id, start_date, end_date, status, validated, mentor_email, activity_note) FROM stdin;
    public          postgres    false    222   9K       ;          0    33014    activity_details 
   TABLE DATA           ~   COPY public.activity_details (id, activity_id, detail_name, detail_desc, detail_link, detail_type, detail_urutan) FROM stdin;
    public          postgres    false    220   �K       >          0    33112    admin 
   TABLE DATA           �   COPY public.admin (email, admin_name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number) FROM stdin;
    public          postgres    false    223   �L       7          0    32976 
   categories 
   TABLE DATA           W   COPY public.categories (id, category_name, category_description, duration) FROM stdin;
    public          postgres    false    216   �O       4          0    24773 
   job_titles 
   TABLE DATA           M   COPY public.job_titles (id, jobtitle_name, jobtitle_description) FROM stdin;
    public          postgres    false    213   LP       2          0    24752    roles 
   TABLE DATA           O   COPY public.roles (id, role_name, role_description, role_platform) FROM stdin;
    public          postgres    false    211   �P       5          0    24785    user 
   TABLE DATA           �   COPY public."user" (email, name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number, progress) FROM stdin;
    public          postgres    false    214   �Q       E           0    0    activities_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.activities_id_seq', 4, true);
          public          postgres    false    217            F           0    0    activities_owned_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.activities_owned_id_seq', 7, true);
          public          postgres    false    221            G           0    0    activity_details_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.activity_details_id_seq', 6, true);
          public          postgres    false    219            H           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 4, true);
          public          postgres    false    215            I           0    0    job_titles_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.job_titles_id_seq', 7, true);
          public          postgres    false    212            J           0    0    roles_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.roles_id_seq', 6, true);
          public          postgres    false    210            �           2606    16530 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public            postgres    false    209            �           2606    33006    activities PK_activities 
   CONSTRAINT     X   ALTER TABLE ONLY public.activities
    ADD CONSTRAINT "PK_activities" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.activities DROP CONSTRAINT "PK_activities";
       public            postgres    false    218            �           2606    33069 $   activities_owned PK_activities_owned 
   CONSTRAINT     d   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "PK_activities_owned" PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "PK_activities_owned";
       public            postgres    false    222            �           2606    33020 $   activity_details PK_activity_details 
   CONSTRAINT     d   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "PK_activity_details" PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "PK_activity_details";
       public            postgres    false    220            �           2606    33118    admin PK_admin 
   CONSTRAINT     Q   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "PK_admin" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public.admin DROP CONSTRAINT "PK_admin";
       public            postgres    false    223            �           2606    32982    categories PK_categories 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT "PK_categories" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT "PK_categories";
       public            postgres    false    216            �           2606    24779    job_titles PK_job_titles 
   CONSTRAINT     X   ALTER TABLE ONLY public.job_titles
    ADD CONSTRAINT "PK_job_titles" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.job_titles DROP CONSTRAINT "PK_job_titles";
       public            postgres    false    213            �           2606    24758    roles PK_roles 
   CONSTRAINT     N   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT "PK_roles" PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.roles DROP CONSTRAINT "PK_roles";
       public            postgres    false    211            �           2606    24791    user PK_user 
   CONSTRAINT     Q   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "PK_user" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT "PK_user";
       public            postgres    false    214            �           1259    33012    IX_activities_category_id    INDEX     Y   CREATE INDEX "IX_activities_category_id" ON public.activities USING btree (category_id);
 /   DROP INDEX public."IX_activities_category_id";
       public            postgres    false    218            �           1259    33080 !   IX_activities_owned_activities_id    INDEX     i   CREATE INDEX "IX_activities_owned_activities_id" ON public.activities_owned USING btree (activities_id);
 7   DROP INDEX public."IX_activities_owned_activities_id";
       public            postgres    false    222            �           1259    33081    IX_activities_owned_user_email    INDEX     c   CREATE INDEX "IX_activities_owned_user_email" ON public.activities_owned USING btree (user_email);
 4   DROP INDEX public."IX_activities_owned_user_email";
       public            postgres    false    222            �           1259    33026    IX_activity_details_activity_id    INDEX     e   CREATE INDEX "IX_activity_details_activity_id" ON public.activity_details USING btree (activity_id);
 5   DROP INDEX public."IX_activity_details_activity_id";
       public            postgres    false    220            �           1259    33129    IX_admin_jobtitle_id    INDEX     O   CREATE INDEX "IX_admin_jobtitle_id" ON public.admin USING btree (jobtitle_id);
 *   DROP INDEX public."IX_admin_jobtitle_id";
       public            postgres    false    223            �           1259    33130    IX_admin_role_id    INDEX     G   CREATE INDEX "IX_admin_role_id" ON public.admin USING btree (role_id);
 &   DROP INDEX public."IX_admin_role_id";
       public            postgres    false    223            �           1259    24802    IX_user_jobtitle_id    INDEX     O   CREATE INDEX "IX_user_jobtitle_id" ON public."user" USING btree (jobtitle_id);
 )   DROP INDEX public."IX_user_jobtitle_id";
       public            postgres    false    214            �           1259    24803    IX_user_role_id    INDEX     G   CREATE INDEX "IX_user_role_id" ON public."user" USING btree (role_id);
 %   DROP INDEX public."IX_user_role_id";
       public            postgres    false    214            �           2606    33007 /   activities FK_activities_categories_category_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities
    ADD CONSTRAINT "FK_activities_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
 [   ALTER TABLE ONLY public.activities DROP CONSTRAINT "FK_activities_categories_category_id";
       public          postgres    false    3214    216    218            �           2606    33070 =   activities_owned FK_activities_owned_activities_activities_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_activities_activities_id" FOREIGN KEY (activities_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 i   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_activities_activities_id";
       public          postgres    false    218    222    3217            �           2606    33075 4   activities_owned FK_activities_owned_user_user_email    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_user_user_email" FOREIGN KEY (user_email) REFERENCES public."user"(email) ON DELETE CASCADE;
 `   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_user_user_email";
       public          postgres    false    3212    214    222            �           2606    33021 ;   activity_details FK_activity_details_activities_activity_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "FK_activity_details_activities_activity_id" FOREIGN KEY (activity_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 g   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "FK_activity_details_activities_activity_id";
       public          postgres    false    218    220    3217            �           2606    33119 %   admin FK_admin_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_job_titles_jobtitle_id";
       public          postgres    false    3208    223    213            �           2606    33124    admin FK_admin_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_roles_role_id";
       public          postgres    false    211    223    3206            �           2606    32988 #   user FK_user_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_job_titles_jobtitle_id";
       public          postgres    false    213    214    3208            �           2606    32993    user FK_user_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_roles_role_id";
       public          postgres    false    214    3206    211            0     x����J�0�}���%M��nA�ѝj$P[�F}}o;(R����mr�! �����>�6��1��Wv;���1��i��Ű���cA3��é 	�����`.�5SY ��K�T!�IQ0�l��i��!q�0uC��4n�CAwi�C'Ϳs�L�聯ǘ��{����R�XA�i^�S����5��?���tݢ!݄�,G���i~Mz�C�{�I���Ԧ�����-��s�B�Z��N���"�Y�_��Y�T��*XMN<着�	��      9   �   x�m�K
�0D��)t�B�(��f�[�'���}E�Bw��yg7����tE��q�UhB��e��쟓�Թ%t�uO/���_�Ѹ�3��AЭEk�D#�AHrQ�;ۈ�bͩ�y�N���
]���V��t�߹7j L�I~���U�      =   �   x���;�0D��)|�D��OI�!hF�&6I�Kp{��H[L�f����;�S�l扶d*c��.��w�h�:Ĺ�,B�|�_���0�Z����$��W��p\��_���2��yV��2����/i�by�ƪͷ���۸'���s�[�.�R���\�      ;   �   x�����0E��W� �c���&&��L�ؖ�H$��*��̽��dC���g�!�dg]	��������N0�l����Th>ٶM<�9��Lx/f���0A�[Ғl�P	[���F��}A��]<yݥ�/�<%�:f�	�*��2H�� �'��ԛb�`��Z��QJ�1l��      >      x�]T�n;]}�";��x{W��n��F�T�E�=g`(��L&������%_��wz����ק��5��������}�Us�3��H��6g��� 9{
	�!�Am4�>��eʉ�KmT�Fg��!b�)^u�as�5^�^����jd!����ӂSk�D�S��bq��>�zx�eC��l�݀([���,+�<T�+Om�c�n��e�����}�RY?L'F٘�ϰ�sS�d!��h�D��J;]&�.�^��>dx�'����ý��3�-c��K��`�{��0}ʹ��~��n76�{�{�7�$��)�:~x˗�=�I�z2��x�����<�)���W]���s�.ڔ�מ�"+H<�,�]уNe��3�ոi������M/<P�<���%M�`���`�eWg��/x����;iD�v�7!J��b�8ݧ@�Q}ꠄw��T�Vs���Sg�h���yo������zN�>U��8���|nE�����=�9UJ�:9��L2��e�� R�cG�M&|k)�S������OH���g���qΈ�s��(_E�"N9�;Ғ<αý�)�^������+|��jW�@a��,v�f�	n��(��W�*e~��L��W����k�&�B8�ԍ�n<���G�)23 h�Km�q�Bp��7P�J����,���(���j�H$�E��D�np1||�4E��jH:����9;��9�\����LP���T�R>�P&����pl��i�X�^$��pHȷ�5�^����(����7����� �v�      7   ^   x��̽� К��&0���{�|*D΄���
6�|Zi~�	jފ�}y��XvH5��I�w��� �����ڪ&�/)�w���ƞ���>�.�      4   �   x���11��� H���j�5�`]싒�ߓ�@����hg7�����4e.n`�- 'Q%�YÓ�Jc�u�D��/I?S�s�2Y[���Y͉����v���4KZ���.#���l��ʄQ�rmb/���{��N      2   �   x�]�;�0��99��"� 3�XX\�*VR����	��e���U�2Q��YTG2�!�/��!O0��Pe\�L���'Τw�?^��֡32�� ��(�͂���?^f�'t��15@u�gO��NmC]BAW%ʌ�Ox�>`�,Úތ���\�      5     x�]UK�%��^�7�J�G�yx=!E
q�������}�e��p�R��������?�������t�}y���?��T�����4��-R�c�&Jmꭥ�&���&��k06�9�2y�)�Xe���סc��m��C�H��'�.��S{��Z�oJV�1p(�	7ݷC���X|F�V�I�+'�5����k��rm����
�qz��bj;����d�)�����V�ٞW'�����nݦ�%�|�E(�+�?"x�ݛ[��G� О��3�#ܨg��tRo�ȡ6��pI^��d7�y�k�������������ט�E��>T�_������:2W�P�;��q�l뱆��b͹A�ZR�d��?b��'9��,�&�t�RZ������z&����\�+�2ΘCH��Cm��NM(�7�Gl�����٤�6P��yD��髡�<"&sp��	$E%e�]�����o���'!�����g���{�a���C��s�`�wΔX:aVQg�����"�O����<{�ٻc�SnCg�F_�5�GD8�'�1��5��C��y�O ��<K��WA y��A����=�0K��QH��� u�\���s�Xw��L�ͷ����e��r�Z4&C�ڤ�h+\�'R���m7&rK�nu�.h9o�%D�2B�
��vGR�������'��I�z�S�Gv��y�lAn3dm���t	ҁ�C�b�ȃסT������9fԁe	�T���Oai���$ߞw~�|{_��]zKx]�`#�&B��{��*$�c�q�~T�Ԃ�	)@}Mz%��Ƥ(�%͵�:Ds����>�D��Q�Z�x���paߺs�c�r�5z��U+<�������l�'��hEA��!���?���^h�-��ä7%�1~E��
���P�R�������)�$}�{vG#@�"&Ύ�GXQ"�n1�4T3(�sa�D��������'
;6�3P�yK�/�{��'�u*�y����Ӈ��s�8     