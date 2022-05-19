PGDMP         ;                z            lms    14.2    14.2 =    G           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            H           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            I           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            J           1262    33290    lms    DATABASE     c   CREATE DATABASE lms WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_Indonesia.1252';
    DROP DATABASE lms;
                postgres    false            �            1259    33291    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            �            1259    33294 
   activities    TABLE     �   CREATE TABLE public.activities (
    id integer NOT NULL,
    activity_name character varying(200) NOT NULL,
    activity_description character varying(200) NOT NULL,
    category_id integer NOT NULL
);
    DROP TABLE public.activities;
       public         heap    postgres    false            �            1259    33299    activities_id_seq    SEQUENCE     �   ALTER TABLE public.activities ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activities_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    210            �            1259    33300    activities_owned    TABLE     �  CREATE TABLE public.activities_owned (
    id integer NOT NULL,
    start_date text NOT NULL,
    end_date text NOT NULL,
    status character varying(50) NOT NULL,
    late boolean NOT NULL,
    mentor_email character varying(200) NOT NULL,
    activity_note character varying(200) NOT NULL,
    user_email character varying(200) NOT NULL,
    activities_id integer NOT NULL,
    category_id integer NOT NULL
);
 $   DROP TABLE public.activities_owned;
       public         heap    postgres    false            �            1259    33305    activities_owned_id_seq    SEQUENCE     �   ALTER TABLE public.activities_owned ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activities_owned_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    212            �            1259    33306    activity_details    TABLE     L  CREATE TABLE public.activity_details (
    id integer NOT NULL,
    activity_id integer NOT NULL,
    detail_name character varying(100) NOT NULL,
    detail_desc character varying(200) NOT NULL,
    detail_link character varying(200) NOT NULL,
    detail_type character varying(100) NOT NULL,
    detail_urutan integer NOT NULL
);
 $   DROP TABLE public.activity_details;
       public         heap    postgres    false            �            1259    33311    activity_details_id_seq    SEQUENCE     �   ALTER TABLE public.activity_details ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activity_details_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    214            �            1259    33312    admin    TABLE     �  CREATE TABLE public.admin (
    email character varying(200) NOT NULL,
    admin_name character varying(100) NOT NULL,
    "passwordHash" bytea,
    "passwordSalt" bytea,
    role_id integer NOT NULL,
    jobtitle_id integer NOT NULL,
    gender character varying(25) NOT NULL,
    birthdate text NOT NULL,
    phone_number character varying(15) NOT NULL,
    active boolean DEFAULT false NOT NULL
);
    DROP TABLE public.admin;
       public         heap    postgres    false            �            1259    33318 
   categories    TABLE     �   CREATE TABLE public.categories (
    id integer NOT NULL,
    category_name character varying(100) NOT NULL,
    category_description character varying(200) NOT NULL,
    duration integer NOT NULL
);
    DROP TABLE public.categories;
       public         heap    postgres    false            �            1259    33323    categories_id_seq    SEQUENCE     �   ALTER TABLE public.categories ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.categories_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    217            �            1259    33324 
   job_titles    TABLE     �   CREATE TABLE public.job_titles (
    id integer NOT NULL,
    jobtitle_name character varying(100) NOT NULL,
    jobtitle_description character varying(200) NOT NULL
);
    DROP TABLE public.job_titles;
       public         heap    postgres    false            �            1259    33329    job_titles_id_seq    SEQUENCE     �   ALTER TABLE public.job_titles ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.job_titles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    219            �            1259    33330    roles    TABLE     �   CREATE TABLE public.roles (
    id integer NOT NULL,
    role_name character varying(50) NOT NULL,
    role_description character varying(200) NOT NULL,
    role_platform character varying(50) NOT NULL
);
    DROP TABLE public.roles;
       public         heap    postgres    false            �            1259    33335    roles_id_seq    SEQUENCE     �   ALTER TABLE public.roles ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.roles_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    221            �            1259    33336    user    TABLE     2  CREATE TABLE public."user" (
    email character varying(200) NOT NULL,
    name character varying(100) NOT NULL,
    "passwordHash" bytea,
    "passwordSalt" bytea,
    role_id integer DEFAULT 0 NOT NULL,
    jobtitle_id integer DEFAULT 0 NOT NULL,
    gender character varying(25) NOT NULL,
    birthdate text NOT NULL,
    phone_number character varying(15) NOT NULL,
    progress double precision NOT NULL,
    active boolean DEFAULT false NOT NULL,
    "assignedActivities" integer DEFAULT 0 NOT NULL,
    "finishedActivities" integer DEFAULT 0 NOT NULL
);
    DROP TABLE public."user";
       public         heap    postgres    false            6          0    33291    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    209   mO       7          0    33294 
   activities 
   TABLE DATA           Z   COPY public.activities (id, activity_name, activity_description, category_id) FROM stdin;
    public          postgres    false    210   �P       9          0    33300    activities_owned 
   TABLE DATA           �   COPY public.activities_owned (id, start_date, end_date, status, late, mentor_email, activity_note, user_email, activities_id, category_id) FROM stdin;
    public          postgres    false    212   �Q       ;          0    33306    activity_details 
   TABLE DATA           ~   COPY public.activity_details (id, activity_id, detail_name, detail_desc, detail_link, detail_type, detail_urutan) FROM stdin;
    public          postgres    false    214   )R       =          0    33312    admin 
   TABLE DATA           �   COPY public.admin (email, admin_name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number, active) FROM stdin;
    public          postgres    false    216   �S       >          0    33318 
   categories 
   TABLE DATA           W   COPY public.categories (id, category_name, category_description, duration) FROM stdin;
    public          postgres    false    217   �X       @          0    33324 
   job_titles 
   TABLE DATA           M   COPY public.job_titles (id, jobtitle_name, jobtitle_description) FROM stdin;
    public          postgres    false    219   Y       B          0    33330    roles 
   TABLE DATA           O   COPY public.roles (id, role_name, role_description, role_platform) FROM stdin;
    public          postgres    false    221   �Y       D          0    33336    user 
   TABLE DATA           �   COPY public."user" (email, name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number, progress, active, "assignedActivities", "finishedActivities") FROM stdin;
    public          postgres    false    223   vZ       K           0    0    activities_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.activities_id_seq', 4, true);
          public          postgres    false    211            L           0    0    activities_owned_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.activities_owned_id_seq', 4, true);
          public          postgres    false    213            M           0    0    activity_details_id_seq    SEQUENCE SET     F   SELECT pg_catalog.setval('public.activity_details_id_seq', 16, true);
          public          postgres    false    215            N           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 4, true);
          public          postgres    false    218            O           0    0    job_titles_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.job_titles_id_seq', 7, true);
          public          postgres    false    220            P           0    0    roles_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.roles_id_seq', 6, true);
          public          postgres    false    222            �           2606    33347 .   __EFMigrationsHistory PK___EFMigrationsHistory 
   CONSTRAINT     {   ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");
 \   ALTER TABLE ONLY public."__EFMigrationsHistory" DROP CONSTRAINT "PK___EFMigrationsHistory";
       public            postgres    false    209            �           2606    33349    activities PK_activities 
   CONSTRAINT     X   ALTER TABLE ONLY public.activities
    ADD CONSTRAINT "PK_activities" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.activities DROP CONSTRAINT "PK_activities";
       public            postgres    false    210            �           2606    33351 $   activities_owned PK_activities_owned 
   CONSTRAINT     d   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "PK_activities_owned" PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "PK_activities_owned";
       public            postgres    false    212            �           2606    33353 $   activity_details PK_activity_details 
   CONSTRAINT     d   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "PK_activity_details" PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "PK_activity_details";
       public            postgres    false    214            �           2606    33519    admin PK_admin 
   CONSTRAINT     Q   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "PK_admin" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public.admin DROP CONSTRAINT "PK_admin";
       public            postgres    false    216            �           2606    33357    categories PK_categories 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT "PK_categories" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT "PK_categories";
       public            postgres    false    217            �           2606    33359    job_titles PK_job_titles 
   CONSTRAINT     X   ALTER TABLE ONLY public.job_titles
    ADD CONSTRAINT "PK_job_titles" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.job_titles DROP CONSTRAINT "PK_job_titles";
       public            postgres    false    219            �           2606    33361    roles PK_roles 
   CONSTRAINT     N   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT "PK_roles" PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.roles DROP CONSTRAINT "PK_roles";
       public            postgres    false    221            �           2606    33460    user PK_user 
   CONSTRAINT     Q   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "PK_user" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT "PK_user";
       public            postgres    false    223            �           1259    33364    IX_activities_category_id    INDEX     Y   CREATE INDEX "IX_activities_category_id" ON public.activities USING btree (category_id);
 /   DROP INDEX public."IX_activities_category_id";
       public            postgres    false    210            �           1259    33365 !   IX_activities_owned_activities_id    INDEX     i   CREATE INDEX "IX_activities_owned_activities_id" ON public.activities_owned USING btree (activities_id);
 7   DROP INDEX public."IX_activities_owned_activities_id";
       public            postgres    false    212            �           1259    33366    IX_activities_owned_category_id    INDEX     e   CREATE INDEX "IX_activities_owned_category_id" ON public.activities_owned USING btree (category_id);
 5   DROP INDEX public."IX_activities_owned_category_id";
       public            postgres    false    212            �           1259    33528    IX_activities_owned_user_email    INDEX     c   CREATE INDEX "IX_activities_owned_user_email" ON public.activities_owned USING btree (user_email);
 4   DROP INDEX public."IX_activities_owned_user_email";
       public            postgres    false    212            �           1259    33368    IX_activity_details_activity_id    INDEX     e   CREATE INDEX "IX_activity_details_activity_id" ON public.activity_details USING btree (activity_id);
 5   DROP INDEX public."IX_activity_details_activity_id";
       public            postgres    false    214            �           1259    33369    IX_admin_jobtitle_id    INDEX     O   CREATE INDEX "IX_admin_jobtitle_id" ON public.admin USING btree (jobtitle_id);
 *   DROP INDEX public."IX_admin_jobtitle_id";
       public            postgres    false    216            �           1259    33370    IX_admin_role_id    INDEX     G   CREATE INDEX "IX_admin_role_id" ON public.admin USING btree (role_id);
 &   DROP INDEX public."IX_admin_role_id";
       public            postgres    false    216            �           1259    33371    IX_user_jobtitle_id    INDEX     O   CREATE INDEX "IX_user_jobtitle_id" ON public."user" USING btree (jobtitle_id);
 )   DROP INDEX public."IX_user_jobtitle_id";
       public            postgres    false    223            �           1259    33372    IX_user_role_id    INDEX     G   CREATE INDEX "IX_user_role_id" ON public."user" USING btree (role_id);
 %   DROP INDEX public."IX_user_role_id";
       public            postgres    false    223            �           2606    33373 /   activities FK_activities_categories_category_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities
    ADD CONSTRAINT "FK_activities_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
 [   ALTER TABLE ONLY public.activities DROP CONSTRAINT "FK_activities_categories_category_id";
       public          postgres    false    217    3225    210            �           2606    33378 =   activities_owned FK_activities_owned_activities_activities_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_activities_activities_id" FOREIGN KEY (activities_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 i   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_activities_activities_id";
       public          postgres    false    3211    212    210            �           2606    33383 ;   activities_owned FK_activities_owned_categories_category_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
 g   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_categories_category_id";
       public          postgres    false    217    212    3225            �           2606    33529 4   activities_owned FK_activities_owned_user_user_email    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_user_user_email" FOREIGN KEY (user_email) REFERENCES public."user"(email) ON DELETE CASCADE;
 `   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_user_user_email";
       public          postgres    false    3233    212    223            �           2606    33393 ;   activity_details FK_activity_details_activities_activity_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "FK_activity_details_activities_activity_id" FOREIGN KEY (activity_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 g   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "FK_activity_details_activities_activity_id";
       public          postgres    false    214    210    3211            �           2606    33398 %   admin FK_admin_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_job_titles_jobtitle_id";
       public          postgres    false    3227    219    216            �           2606    33403    admin FK_admin_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_roles_role_id";
       public          postgres    false    3229    221    216            �           2606    33408 #   user FK_user_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_job_titles_jobtitle_id";
       public          postgres    false    223    219    3227            �           2606    33413    user FK_user_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_roles_role_id";
       public          postgres    false    3229    221    223            6   g  x���]K�0�k��,󙶗��x�q'��4P[��s��;��hMz���ɼC	���CP�c9^)�П�-liCT�M܏áBX�Ň�s*B�����$�?�dSE8l�� Y[p��֧	�G��	�aG���Ҽ $�i���}�i֬E���:-���ZI;@�g|�M����ϊ�)��+`�'�����1�X@����>�v^c�ؐ.�|V"Ԁb�ݧJ��P���h�or�/�7j��5�JS^C����3L�fH��W��$���"�LUX/�%��KûS�5)�\]�d�,z�����ؿ<�K�+p�6�yϦ���ǫ8<>�bE���՟�Eȡ��3+�B��TA9      7   �   x�m�K
�0D��)t�B�(��f�[�'���}E�Bw��yg7����tE��q�UhB��e��쟓�Թ%t�uO/���_�Ѹ�3��AЭEk�D#�AHrQ�;ۈ�bͩ�y�N���
]���V��t�߹7j L�I~���U�      9   �   x�3�4202�50�54B0�9��3��RS8K8sS�J�R+srR���s99��3�PD�9���)�Y \��!V\���YR4>�3%�2�,����"Ӎ�ezJbZ&��	�t#�L�4�7�4����� �cJ      ;   G  x���Mn� ���)8�m��4٥��v)QV��i ��dp�ܾ��i�.*u�4�c���AA~G��]/���u��=*Ė�c��[���kt�^�8�L�<�W����$
�AFf�й)`c���4NX68�WasXljl��CA�����Ps ��>�h�X�w�Bp~����a����K��t�����J�<�<+ٌMYVr���=(їG߭�ҵ�Nn�N�����8n�����1���ֲ�bᇁ0��1|�(y9�0&$�>�XE�I�Yں�����E�"d���H+��4)�{�sJb�b�M����Jv�.f���)^B�'����      =     x�u�A�GD׭S�m��L2��f1��Ԇ�$C#Y�l����z64���h��_�+�������%�ǟ?�P��������/_��u�;yy��w�ÕW�s*Rj��[oж3���ۇ����w�����xN;bgoEsk��C"��j\�r���A��{����!��.#���/�Q��=(��<y���Ȅ|/g�%��y��놚�?&R���\��ҐR帜N�9$}�S)�Hs��9k�˚w-��h�>�JM��q�J|x���a{�>���7�j�:!kM��B�Ow�n���h�#~0�����>~�->��;�����e̥��^�ݏ�u���g�67�����46�YN;wҺ�A�%�I2��dXvǲu�V����F���I(���B�wiet�ip�\q�ף!͓�I$������� +`&��r/L��P�Fv�*���z�nZp��Tŕ8�����n2�w��9r�Q%��2ݜ��.�邹MW����~�NYH�Bneè��S�2��s�zF�y��y�mpdD���lgꃌ�f`�<)������Y��������?V�+��V�������K|c��5�Rr5O�01�ZZ��a��m����fLɢ2ry��L����gl&p�/��)�\hu��L������y4Z�7�c�ș�s���9\Zl�D��H>:�I�q�q5��[w�벡S��B_���[Uc:����.�x��<K�A�@�.�Q�kc�(>^}����4Q㙆�t!��Ǧ̶{1���8���zJ���5�ޭD����*�<5_��)������������_>|c�O�|������)��914f���7���M1�Ɲm|"�2�89@"Ai�3_YؑM�U�m�.2�T�f�Z�?h��e�I���{� �f�<J���j8�J�A�	��^{z���[aH��]	ԋ�F���4M��WL {�9�����o�LO�����2�w��� f�`l`Ƀ&��I8�S�{���H�'�W]��l�����&�~|����D~��(-6�K0�����W!����N����a�3`)2� �<Qv%P\|�>�D/���D����E=�>��U��*�0(|�8�
�t��V�xh�:�gX����8Q�q�^��Țw+����}H��<s�Pt��`T���CS����"2�q �@9{����zoT
�� [8���z2�H��!��{���&�w*���X����4@�m��+��pէ�%��;x\U�l�b��B�+?��/~��a���o����w���	�)ti      >   ^   x��̽� К��&0���{�|*D΄���
6�|��/)N�[�//���f�}0�@�����R9����ƞt+c��!z�������>�g.�      @   �   x���11��� H���$:��.v�$w�ߓ���jW��ݹ+a8�L1e*n �	Z�e�*�`�ބU��J��$]����%Yۜ�.%q��3͑����
��{k�Y��~�/1fXq�D� (�&��z�U�Nx      B   �   x�]�;�0@��> ���̨b``aq�XI�*�ǴP�e���9Ց���bZ��B�BM�
)�Q�}�8�Q
�9S���ݘ�Ã�A�)2���� ��Q�:s�Y�;sxS�3
zu3���q�G�"&�қ6v�n_���#-d���R��Kc�}y\�      D   !  x�mV��$��g��/pV�,3�N�]BY�`� K����z��/f�ޞ.��������ܿ��������������˗_��p��#ץu��s�x��[��y�n�[Ԣ��;7�Ȍ]4�GQ�X߼7�4kK����:c�!^i�s��-��&՝\�c�p�Cu.�s�KYe&�>ﾧկ�<�Gw9m?E�=���%�+w�0�1ǽ��ckGU6��2c��-.gL�^]��]kEg��T��I���쎾=����xQ���T�f��7Nz�OɬiS}��S�ko��~w�,�u�Ƌ_�������%����D��5&�.����śު�_�����c+�� I��c�]֒����j�r�:*���F��-�`�c`H�6��`$@AC Wd�Ͷ�{g�2�BFs��k�,��a�aT�d��4~�ʠ&n?W�6X�k�+e7�z��vy��t�-�\.�d��r���5V�ʕ^}6tq!UQ_����@��j�{�2�$6�֠�ETB&C���+�ƹ���G�3��p
Sb�S���b�����˾T�-��ç��5����I�Q��oT���{&q��a?�j1�C����]2K�gC�80�dഀuU���=�:qWL�Jd��g�PU1�d��ˈ �L�A���>?�0�r`W]1� *öM��kIz�mF��n�s!Xh-`����M	���p�D,��d[ #��3�3L�z��"1
���b����##����[/qB���d1�-�1H���������H��jL�mF�5�b���o
@���z���>^�'��A/}��?�}��ᆑ�8
n̆�r5�^�� hnSK,�7��9��S���ұ҅�%oL `b!F���V6���{�C�� j��H^q�Ig���3������1TUC(f��3��vm�U.���Q�@r����L��c��0Š���62qMbv{t�ڶ-.,�4ȁ�5�?D�P{�d!r1�����;�0��u�a���]s,����pH^��HU���;.�����\�߸�o�������|�K�7���e,���L����H;O���TH\�d?�Fs�2ָ�����Z��^���s���(�=�?p02��l,~�>f@�E	����J d���qx�Uu��	a>��ɀ� BnM�J�NxO�Gg�(B���j�l������S^01�ó���X35y�����O2\�D�^B&�օ>R�ط!���
�����_���Y�R(K䁏C]���y�pE�����������H�����ooo��qq�     