import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:http/http.dart' as http;
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_bloc.dart';
import 'package:shared_preferences/shared_preferences.dart';
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/email/email_page.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_page.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_page.dart';
import 'package:mobiclone/pages/sign_up/sign_up_page.dart';
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_page.dart';
import 'package:mobiclone/services/user_service.dart';
import 'package:mobiclone/services/session_service.dart';

void main() async {
  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
    statusBarColor: Colors.white,
  ));

  runApp(MobicloneApp());

  SharedPreferences preferences = await SharedPreferences.getInstance();

  http.Client client = http.Client();

  kiwi.Container()
    ..registerInstance(preferences)
    ..registerInstance(client)
    ..registerFactory(
      (c) => UserService(c.resolve()),
    )
    ..registerFactory(
      (c) => SessionService(c.resolve()),
    )
    ..registerFactory(
      (c) => UserRepository(c.resolve()),
    )
    ..registerFactory(
      (c) => NameBloc(c.resolve()),
    )
    ..registerFactory(
      (c) => EmailBloc(c.resolve()),
    )
    ..registerFactory(
      (c) => PasswordBloc(
        c.resolve(),
        c.resolve(),
      ),
    )
    ..registerFactory(
      (c) => SignInWithEmailBloc(c.resolve()),
    );
}

class MobicloneApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      routes: {
        '/': (context) => SignUpPage(),
        '/name': (context) => NamePage(),
        '/email': (context) => EmailPage(),
        '/password': (context) => PasswordPage(),
        '/sign-in-with-email': (context) => SignInWithEmailPage(),
      },
      theme: ThemeData(
        primaryColor: Colors.deepPurple[900],
        accentColor: Colors.deepPurpleAccent[700],
        buttonTheme: ButtonThemeData(
          buttonColor: Colors.deepPurpleAccent[700],
          textTheme: ButtonTextTheme.primary,
        ),
      ),
    );
  }
}
