import 'package:flutter/material.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:mobiclone/pages/password/password_bloc.dart';
import 'package:mobiclone/pages/password/password_event.dart';
import 'package:mobiclone/pages/password/password_state.dart';

class PasswordPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider<PasswordBloc>(
      create: (_) => kiwi.Container().resolve<PasswordBloc>(),
      child: Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.white,
          elevation: 0,
          iconTheme: IconThemeData(
            color: Colors.deepPurple,
          ),
        ),
        backgroundColor: Colors.white,
        body: PasswordForm(),
      ),
    );
  }
}

class PasswordForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => PasswordFormState();
}

class PasswordFormState extends State<PasswordForm> {
  final TextEditingController _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BlocListener<PasswordBloc, PasswordState>(
      listener: (context, state) {
        if (state is ValidatedPasswordState) {
          Navigator.of(context).popUntil((routes) => routes.isFirst);
        }

        if (state is RequiredPasswordState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }
      },
      child: BlocBuilder<PasswordBloc, PasswordState>(
        builder: (context, state) {
          return SafeArea(
            child: Column(
              mainAxisAlignment: MainAxisAlignment.spaceAround,
              crossAxisAlignment: CrossAxisAlignment.start,
              children: <Widget>[
                Padding(
                  padding: EdgeInsets.all(16.0),
                  child: Text(
                    'Enter your password',
                    style: TextStyle(
                      color: Colors.black54,
                      fontSize: 36,
                    ),
                  ),
                ),
                Expanded(
                  flex: 1,
                  child: Padding(
                    padding: EdgeInsets.all(16.0),
                    child: TextField(
                      textAlignVertical: TextAlignVertical.center,
                      controller: _passwordController,
                      decoration: InputDecoration(
                        border: InputBorder.none,
                        hintText: 'strong password',
                        hintStyle: TextStyle(
                          color: Colors.black26,
                        ),
                      ),
                      style: TextStyle(
                        fontSize: 36.0,
                        color: Colors.black54,
                      ),
                      maxLines: null,
                      expands: true,
                    ),
                  ),
                ),
                RaisedButton(
                  shape: RoundedRectangleBorder(
                    borderRadius: BorderRadius.circular(0),
                  ),
                  padding: EdgeInsets.all(24.0),
                  onPressed: () {
                    BlocProvider.of<PasswordBloc>(context).add(
                      PasswordEventSubmit(_passwordController.text),
                    );
                  },
                  child: Row(
                    children: <Widget>[
                      Text(
                        'Finish',
                        style: TextStyle(
                          fontSize: 20.0,
                        ),
                      ),
                    ],
                  ),
                )
              ],
            ),
          );
        },
      ),
    );
  }
}
