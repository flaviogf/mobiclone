import 'package:flutter/material.dart';
import 'package:flutter/services.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/pages/email/email_bloc.dart';
import 'package:mobiclone/pages/name/name_bloc.dart';
import 'package:mobiclone/pages/sign_up/sign_up_page.dart';

void main() {
  kiwi.Container()..registerInstance(NameBloc())..registerInstance(EmailBloc());

  SystemChrome.setSystemUIOverlayStyle(SystemUiOverlayStyle(
    statusBarColor: Colors.white,
  ));

  runApp(MobicloneApp());
}

class MobicloneApp extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return MaterialApp(
      debugShowCheckedModeBanner: false,
      theme: ThemeData(
        primaryColor: Colors.deepPurple[900],
        accentColor: Colors.deepPurpleAccent[700],
        buttonTheme: ButtonThemeData(
          buttonColor: Colors.deepPurpleAccent[700],
          textTheme: ButtonTextTheme.primary,
        ),
      ),
      home: SignUpPage(),
    );
  }
}
