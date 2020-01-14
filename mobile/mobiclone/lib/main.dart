import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/data/user_repository.dart';
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/email/email_page.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/name/name_page.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_page.dart';
import 'package:mobiclone/pages/sign_up/sign_up_page.dart';
import 'package:mobiclone/services/user_service.dart';
import 'package:shared_preferences/shared_preferences.dart';

void main() async {
  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
    statusBarColor: Colors.white,
  ));

  runApp(MobicloneApp());

  SharedPreferences preferences = await SharedPreferences.getInstance();

  kiwi.Container()
    ..registerInstance(preferences)
    ..registerFactory(
      (_) => UserService(),
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
        '/password': (context) => PasswordPage()
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
