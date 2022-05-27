PGDMP         )                z            lms    14.2    14.2 =    K           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            L           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            M           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            N           1262    33290    lms    DATABASE     c   CREATE DATABASE lms WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE = 'English_Indonesia.1252';
    DROP DATABASE lms;
                postgres    false            �            1259    33291    __EFMigrationsHistory    TABLE     �   CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);
 +   DROP TABLE public."__EFMigrationsHistory";
       public         heap    postgres    false            �            1259    33294 
   activities    TABLE     S  CREATE TABLE public.activities (
    id integer NOT NULL,
    activity_name character varying(200) NOT NULL,
    activity_description character varying(200) NOT NULL,
    category_id integer,
    cover character varying(200) DEFAULT ''::character varying NOT NULL,
    type character varying(100) DEFAULT ''::character varying NOT NULL
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
            public          postgres    false    212            �            1259    33632    activity_details    TABLE     C  CREATE TABLE public.activity_details (
    id integer NOT NULL,
    detail_name character varying(100) NOT NULL,
    detail_desc character varying(500) NOT NULL,
    detail_link character varying(200),
    detail_type character varying(100) NOT NULL,
    detail_urutan integer NOT NULL,
    activity_id integer NOT NULL
);
 $   DROP TABLE public.activity_details;
       public         heap    postgres    false            �            1259    33631    activity_details_id_seq    SEQUENCE     �   ALTER TABLE public.activity_details ALTER COLUMN id ADD GENERATED BY DEFAULT AS IDENTITY (
    SEQUENCE NAME public.activity_details_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1
);
            public          postgres    false    223            �            1259    33312    admin    TABLE     �  CREATE TABLE public.admin (
    email character varying(200) NOT NULL,
    admin_name character varying(100) NOT NULL,
    "passwordHash" bytea,
    "passwordSalt" bytea,
    role_id integer NOT NULL,
    jobtitle_id integer NOT NULL,
    gender character varying(25) NOT NULL,
    birthdate text NOT NULL,
    phone_number character varying(15) NOT NULL,
    active boolean DEFAULT false NOT NULL,
    photo character varying(200) DEFAULT ''::character varying NOT NULL
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
            public          postgres    false    215            �            1259    33324 
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
            public          postgres    false    217            �            1259    33330    roles    TABLE     �   CREATE TABLE public.roles (
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
            public          postgres    false    219            �            1259    33336    user    TABLE     {  CREATE TABLE public."user" (
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
    "finishedActivities" integer DEFAULT 0 NOT NULL,
    photo character varying(200) DEFAULT ''::character varying NOT NULL
);
    DROP TABLE public."user";
       public         heap    postgres    false            :          0    33291    __EFMigrationsHistory 
   TABLE DATA           R   COPY public."__EFMigrationsHistory" ("MigrationId", "ProductVersion") FROM stdin;
    public          postgres    false    209   �P       ;          0    33294 
   activities 
   TABLE DATA           g   COPY public.activities (id, activity_name, activity_description, category_id, cover, type) FROM stdin;
    public          postgres    false    210   IR       =          0    33300    activities_owned 
   TABLE DATA           �   COPY public.activities_owned (id, start_date, end_date, status, late, mentor_email, activity_note, user_email, activities_id, category_id) FROM stdin;
    public          postgres    false    212   iS       H          0    33632    activity_details 
   TABLE DATA           ~   COPY public.activity_details (id, detail_name, detail_desc, detail_link, detail_type, detail_urutan, activity_id) FROM stdin;
    public          postgres    false    223   T       ?          0    33312    admin 
   TABLE DATA           �   COPY public.admin (email, admin_name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number, active, photo) FROM stdin;
    public          postgres    false    214   U       @          0    33318 
   categories 
   TABLE DATA           W   COPY public.categories (id, category_name, category_description, duration) FROM stdin;
    public          postgres    false    215   y]       B          0    33324 
   job_titles 
   TABLE DATA           M   COPY public.job_titles (id, jobtitle_name, jobtitle_description) FROM stdin;
    public          postgres    false    217   �]       D          0    33330    roles 
   TABLE DATA           O   COPY public.roles (id, role_name, role_description, role_platform) FROM stdin;
    public          postgres    false    219   �^       F          0    33336    user 
   TABLE DATA           �   COPY public."user" (email, name, "passwordHash", "passwordSalt", role_id, jobtitle_id, gender, birthdate, phone_number, progress, active, "assignedActivities", "finishedActivities", photo) FROM stdin;
    public          postgres    false    221   C_       O           0    0    activities_id_seq    SEQUENCE SET     @   SELECT pg_catalog.setval('public.activities_id_seq', 49, true);
          public          postgres    false    211            P           0    0    activities_owned_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.activities_owned_id_seq', 7, true);
          public          postgres    false    213            Q           0    0    activity_details_id_seq    SEQUENCE SET     E   SELECT pg_catalog.setval('public.activity_details_id_seq', 3, true);
          public          postgres    false    222            R           0    0    categories_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.categories_id_seq', 4, true);
          public          postgres    false    216            S           0    0    job_titles_id_seq    SEQUENCE SET     ?   SELECT pg_catalog.setval('public.job_titles_id_seq', 7, true);
          public          postgres    false    218            T           0    0    roles_id_seq    SEQUENCE SET     :   SELECT pg_catalog.setval('public.roles_id_seq', 6, true);
          public          postgres    false    220            �           2606    33347 .   __EFMigrationsHistory PK___EFMigrationsHistory 
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
       public            postgres    false    212            �           2606    33638 $   activity_details PK_activity_details 
   CONSTRAINT     d   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "PK_activity_details" PRIMARY KEY (id);
 P   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "PK_activity_details";
       public            postgres    false    223            �           2606    33519    admin PK_admin 
   CONSTRAINT     Q   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "PK_admin" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public.admin DROP CONSTRAINT "PK_admin";
       public            postgres    false    214            �           2606    33357    categories PK_categories 
   CONSTRAINT     X   ALTER TABLE ONLY public.categories
    ADD CONSTRAINT "PK_categories" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.categories DROP CONSTRAINT "PK_categories";
       public            postgres    false    215            �           2606    33359    job_titles PK_job_titles 
   CONSTRAINT     X   ALTER TABLE ONLY public.job_titles
    ADD CONSTRAINT "PK_job_titles" PRIMARY KEY (id);
 D   ALTER TABLE ONLY public.job_titles DROP CONSTRAINT "PK_job_titles";
       public            postgres    false    217            �           2606    33361    roles PK_roles 
   CONSTRAINT     N   ALTER TABLE ONLY public.roles
    ADD CONSTRAINT "PK_roles" PRIMARY KEY (id);
 :   ALTER TABLE ONLY public.roles DROP CONSTRAINT "PK_roles";
       public            postgres    false    219            �           2606    33460    user PK_user 
   CONSTRAINT     Q   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "PK_user" PRIMARY KEY (email);
 :   ALTER TABLE ONLY public."user" DROP CONSTRAINT "PK_user";
       public            postgres    false    221            �           1259    33364    IX_activities_category_id    INDEX     Y   CREATE INDEX "IX_activities_category_id" ON public.activities USING btree (category_id);
 /   DROP INDEX public."IX_activities_category_id";
       public            postgres    false    210            �           1259    33365 !   IX_activities_owned_activities_id    INDEX     i   CREATE INDEX "IX_activities_owned_activities_id" ON public.activities_owned USING btree (activities_id);
 7   DROP INDEX public."IX_activities_owned_activities_id";
       public            postgres    false    212            �           1259    33366    IX_activities_owned_category_id    INDEX     e   CREATE INDEX "IX_activities_owned_category_id" ON public.activities_owned USING btree (category_id);
 5   DROP INDEX public."IX_activities_owned_category_id";
       public            postgres    false    212            �           1259    33528    IX_activities_owned_user_email    INDEX     c   CREATE INDEX "IX_activities_owned_user_email" ON public.activities_owned USING btree (user_email);
 4   DROP INDEX public."IX_activities_owned_user_email";
       public            postgres    false    212            �           1259    33644    IX_activity_details_activity_id    INDEX     e   CREATE INDEX "IX_activity_details_activity_id" ON public.activity_details USING btree (activity_id);
 5   DROP INDEX public."IX_activity_details_activity_id";
       public            postgres    false    223            �           1259    33369    IX_admin_jobtitle_id    INDEX     O   CREATE INDEX "IX_admin_jobtitle_id" ON public.admin USING btree (jobtitle_id);
 *   DROP INDEX public."IX_admin_jobtitle_id";
       public            postgres    false    214            �           1259    33370    IX_admin_role_id    INDEX     G   CREATE INDEX "IX_admin_role_id" ON public.admin USING btree (role_id);
 &   DROP INDEX public."IX_admin_role_id";
       public            postgres    false    214            �           1259    33371    IX_user_jobtitle_id    INDEX     O   CREATE INDEX "IX_user_jobtitle_id" ON public."user" USING btree (jobtitle_id);
 )   DROP INDEX public."IX_user_jobtitle_id";
       public            postgres    false    221            �           1259    33372    IX_user_role_id    INDEX     G   CREATE INDEX "IX_user_role_id" ON public."user" USING btree (role_id);
 %   DROP INDEX public."IX_user_role_id";
       public            postgres    false    221            �           2606    33373 /   activities FK_activities_categories_category_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities
    ADD CONSTRAINT "FK_activities_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
 [   ALTER TABLE ONLY public.activities DROP CONSTRAINT "FK_activities_categories_category_id";
       public          postgres    false    3226    215    210            �           2606    33378 =   activities_owned FK_activities_owned_activities_activities_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_activities_activities_id" FOREIGN KEY (activities_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 i   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_activities_activities_id";
       public          postgres    false    3215    212    210            �           2606    33383 ;   activities_owned FK_activities_owned_categories_category_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_categories_category_id" FOREIGN KEY (category_id) REFERENCES public.categories(id) ON DELETE CASCADE;
 g   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_categories_category_id";
       public          postgres    false    3226    215    212            �           2606    33529 4   activities_owned FK_activities_owned_user_user_email    FK CONSTRAINT     �   ALTER TABLE ONLY public.activities_owned
    ADD CONSTRAINT "FK_activities_owned_user_user_email" FOREIGN KEY (user_email) REFERENCES public."user"(email) ON DELETE CASCADE;
 `   ALTER TABLE ONLY public.activities_owned DROP CONSTRAINT "FK_activities_owned_user_user_email";
       public          postgres    false    212    3234    221            �           2606    33639 ;   activity_details FK_activity_details_activities_activity_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.activity_details
    ADD CONSTRAINT "FK_activity_details_activities_activity_id" FOREIGN KEY (activity_id) REFERENCES public.activities(id) ON DELETE CASCADE;
 g   ALTER TABLE ONLY public.activity_details DROP CONSTRAINT "FK_activity_details_activities_activity_id";
       public          postgres    false    223    3215    210            �           2606    33398 %   admin FK_admin_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_job_titles_jobtitle_id";
       public          postgres    false    214    3228    217            �           2606    33403    admin FK_admin_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public.admin
    ADD CONSTRAINT "FK_admin_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public.admin DROP CONSTRAINT "FK_admin_roles_role_id";
       public          postgres    false    3230    214    219            �           2606    33408 #   user FK_user_job_titles_jobtitle_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_job_titles_jobtitle_id" FOREIGN KEY (jobtitle_id) REFERENCES public.job_titles(id) ON DELETE CASCADE;
 Q   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_job_titles_jobtitle_id";
       public          postgres    false    3228    217    221            �           2606    33413    user FK_user_roles_role_id    FK CONSTRAINT     �   ALTER TABLE ONLY public."user"
    ADD CONSTRAINT "FK_user_roles_role_id" FOREIGN KEY (role_id) REFERENCES public.roles(id) ON DELETE CASCADE;
 H   ALTER TABLE ONLY public."user" DROP CONSTRAINT "FK_user_roles_role_id";
       public          postgres    false    3230    219    221            :   �  x����j1���a�h{�6��@B�����`�*^%�o��5	�Fr�w�<�36"�i3�����o����TkX�
?	��G��CW �Bl]���`"2�i�d#�A<�� *S��!i��W�>U�~�(j������5FnNaL���.�8,Yc�4��4����uJ���ϻ�z�{���TB�x4<��ؽ��K<�l���>�=/17kH[?��� �����ݟ�w�G�u���1��>~�:�jIoØ�P�.���q�!۬����Z�D��՚�)
�Eg	Rٹ��iǒ�I.��u�"ӽ�&��o��%�/b��4�i�Ɲ{���^^S�"C�DbOd�+16|Ί!��%�A���pS��~��i�_/)���m���D�Ƴ���*��kO�9�k�Z����x�      ;     x����j�0���S�	��I��^;�r�4�V�����SC`�m��$����R4x� F�cH '^�i�`���hG�����d�Ɉ��Hrڮ���B�H����łƽʕ�k�TQ%��C^v��.�f������/F��	"c����-�4Ilb�fluųb+���$���8=�a����)�s@��j�	@L���b�!���uۻ��:��3�:/��:O�:=��m�0.l�]9��	��{XVz�z1��y��U�x���.˲/k]�X      =   �   x����
�0��s�{%V����K7�+�U�
�ۯ6�2s�C�!H�2�:Cu@uD��n�Y�gkB�z��݉��N7�/������P��_z�{�)U��O�%F���
��W굪e��fp�,޸!#!�1y�"6{D4]�%h�!��K�]      H   �   x����n� ���)�)H�G�e��k/�H�QP ����͡ˈ��l}���`��t��;�ͺ�A���gp��>��73��#r�%?�>�ɬ��4�t��d��H����-P$T�P��*ЖP��B�x��5�>`�MBUQͻU��%Lv�	>�2���T:A��sg�Cq��w���E.n��d��-5�~KȆgi��N���-��la�K��*���(�l��;�a�F�a�����6��_U��4      ?   R  x���˒G�ף��Hά{����@,�ɬʒ��%$��� 3&�Ɩ�Q�������K�-~?ػ��W�û����W��&>}~��v�{�����S�X��}�m�Yw�SO��0͕�5>RO�����I9E�ך��5RY��Qwv�6|��f�s��J�zJɞ�3�5��Z��.e̾S>ܛF�|o�������u�f����8��m(-y�vb��Z��9j5��KG�=f)�{�-�ʔՇ�:��yloM��c�9�{1飖�����H�U�骽6#�3?N~�OQs��DyPr�k�)�;5)�4/|x��Z���ƑxJO��������S���^&y��K��)��<�Ƿ_����������IR��u���W����wo�?��q��"Cgf$���n2���Ql-y�ʿdk�:u����Ȓ�<V��q\�y ���Xǳ�>�z�����l�Ϻ�3�]�ڝa����%WZ��TunƂRM<��'�l����r<�H�+硋;?�h�̽m��Q����[��Z�#��3pV�C��OQ�Nj�RZ�9S�d�y���笶6�ߝtIgZ_�ΖdͼJ�&'j�	Y�G���1���t�Y��K_2��u�7�uNy��R�&��8�@�����������7/���}�g�t�W�#������*d�L�k`/��p�Y��3�Vl)#�@���ۮ���d��V����S��C��� '5w�¦����ؼ��2���9�l
�@�-esEG�JWc�͓0w�<�O�[{R�0Qv�g��i�Y���K����0�9jQ-��3cF)XD	�6�_3����O��k
-��T����Q��¬��5X�/|�'TJQ8u0�+��=CC����S������r���	,8H/��
Ư����A��^���{���A�J�w���kc� [�oaz�T�c�85V�}e�=)��d��=Sj���uf�h��������m��jh���c�IY�5��zV��Z�f]$������11S��R��%{H�G����$w��@!Ƶ�yp�o^�H�~f�k{�nP����o���+���#�Z�N��گ%��AL@�b����� �9tX�p�4��YA�W76�$ٌ2!�t�B<[�d�/��+�gL��_�����Vu��`i?�7��h��&j\�����N�q��9���r�J"K"���N	��[��w����C~�ϣ:�����=��-͚���3�e���D�M�#�UQ2�1���V}��x#�����I�0o�┓�D �r�<�,u�c�r�ZO�F�ȰUS��Y:��>����{�, mm�5����p��=2t��t�T��p�����z����<k}�������g����o��ا��������|W\��r�=¯��wg���[�d)=�`D�._|���
��s��I�@���6H<:�=�ּ�w�}I�{` 	X?@���ᰴ�����r�����|~n�Z{����7L�Dv�A4
��qB(QaO�MA��ǙR�Y����P:G���vþ�Z�`
0��8�Z��z��b[��6���l���}w(�9�>l~5��v��)�G܋N����1����<U����=��61����ۯh�˷���pW;h <���(1�#���Yd�M�>x7%Qi޷˶�z��1SR�]$�'ǒm{��H��g�p%5�R
w���b�΀@?EɥQ��|����i��h���ӰJ��G��/�Ơ�%X�KN.�Y�>6�n�W���@����
�4})��s�(�:�j���/*�R��� u	���,Ò�M�K�����Vo}_̆C� F�:+���砑QW	x<��%V��I��E[��K�'D�+C%�[��_�}���1�t�.2菤e���}�`mWz_���7�7��b�Hi0"=�	 �9�� �S�SoVg�E�U���Sakb��J�%
�LZ�6K^A�>�w�M���pau���Id���T�0h�	�Y��Xy�\Iy���Fy;�}i�L��+p��  �I����j*�;��ڴS�碁�����j�`��d�a�B"<��(zV"�vlL�R����&5p�e�(ձ�*����2<����o^�x��:C0�      @   ^   x��̽� К��&0���{�|*D΄���
6�|��/)N�[�//���f�}0�@�����R9����ƞt+c��!z�������>�g.�      B   �   x���11��� H���$:��.v�$w�ߓ���jW��ݹ+a8�L1e*n �	Z�e�*�`�ބU��J��$]����%Yۜ�.%q��3͑����
��{k�Y��~�/1fXq�D� (�&��z�U�Nx      D   �   x�]�M
�0��ur�9����(
.ܸ���I'%?��wl�wa��'[s�#%8��Ŵ$=�2���R���*(p(�s�.s!�3��wNL�@Rb�L/|�4�kf�Y����`bu��~p�� ���s�L� ����K�a@�Q�뗙
�"���5��7�v`�      F   ]  x��V�nE]_?`����];�H,�"v�TuW� N�$�{�8,p$�����s����;λo�x|��և��ƅۛ7X*����D^D����٤o=��ij�Edjo�b�l����h��^q,b��.�Q}��*�[��u����K)s�Sx�5��U�<\p�
*�買9�"�E�Xe���9Ǡ�9NtBl���>���.��!Ƙ�Ɣ�ro��unjΤء&{*G��i������,�A{�<S����&��A��l#�G.<���#V�*�[���3�͘�O
X:I�nrk���w��ۭ���׭u�1�Fz�|kt�[<�{�����o��o���맇��o�v��_}]�P.����)�䅵�GI�ݞ�{�2'Z�Dv04hU?��ӆ*F��:��k�ʃ=���aY53Y�qXs��gD�<3m��F�%�HA�b���1O걖�4�b@=���qx�RwhQ.)�݂�dKˑ�V;�;AК���V����jl�w�ZPv��3�$��'��ahlXA��&m�Dτ6�۩�����d�0%��w�8�:�.��3  !���_3M������4x&|�͏���/x�r�y�c�yM�r�Nw���ļ�QP=���D���\-3`�N�6��(���b-� G�\;g Cb�i"� ՠ��k�3 (����h#f ��r7��<�Ҿ�x�i^��0��B��P�	��k���Qmj��,�%kC��
�����W���B�q��2D+�a�Z%ի�Nusա.�i�4^>f���t�[�ϋ\HF�����Mp5b���c=>��5LqO���������~���~���7�?��o�[���֤6ה�@��i3�b�����������U~1tY�J|�os�萁,D�o,^f��ȹFm���Xd.�0�=S����CIb*�s$� �l�����m� ��X��.>6P�(�2��@�|��z`!*0�>�0�U�N�3�)*��x�9� ���qˠLf��B�@�<�YWS�*b�3�����/$L��>d�ȯ�'!�p���A�ࣲ����}���>�ۼ��!���۝:��;BY�7�??�������q�)�r��W�!h0@L��m��CG��P���k�l ��Q��G:� b+O()����}����8�M�=�nTh:�_�p�2�-��\pn,�6蠻%�V;6�T���jz��@�q��>V��:p,2ah
C�GC�������t��a0vGzogF�;�"����#W�t1��!v����x�m��N鉘�U&&��C�(�y��,�vA���_�v��Q��?m������/�W��     