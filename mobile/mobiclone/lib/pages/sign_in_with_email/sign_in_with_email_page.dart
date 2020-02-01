import 'package:flutter/material.dart';
import 'package:flutter_bloc/flutter_bloc.dart';
import 'package:kiwi/kiwi.dart' as kiwi;
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_bloc.dart';
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_event.dart';
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_state.dart';

class SignInWithEmailPage extends StatelessWidget {
  @override
  Widget build(BuildContext context) {
    return BlocProvider<SignInWithEmailBloc>(
      create: (_) => kiwi.Container().resolve<SignInWithEmailBloc>(),
      child: Scaffold(
        appBar: AppBar(
          backgroundColor: Colors.white,
          iconTheme: IconThemeData(
            color: Colors.deepPurple,
          ),
          elevation: 0.0,
        ),
        backgroundColor: Colors.white,
        body: SignInWithEmailForm(),
      ),
    );
  }
}

class SignInWithEmailForm extends StatefulWidget {
  @override
  State<StatefulWidget> createState() => SignInWithEmailFormState();
}

class SignInWithEmailFormState extends State<SignInWithEmailForm> {
  final TextEditingController _emailController = TextEditingController();
  final TextEditingController _passwordController = TextEditingController();

  @override
  Widget build(BuildContext context) {
    return BlocListener<SignInWithEmailBloc, SignInWithEmailState>(
      listener: (context, state) {
        if (state is EmailRequiredSignInWithEmailState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }

        if (state is PasswordRequiredSignInWithEmailState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }

        if (state is UnauthorizedSignInWithEmailState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text(state.error)),
          );
        }

        if (state is AuthorizedSignInWithEmailState) {
          Scaffold.of(context).showSnackBar(
            SnackBar(content: Text('Welcome.')),
          );
        }
      },
      child: BlocBuilder<SignInWithEmailBloc, SignInWithEmailState>(
        builder: (context, state) {
          return SafeArea(
            child: SingleChildScrollView(
              padding: EdgeInsets.all(16.0),
              child: Column(
                children: <Widget>[
                  Center(
                    child: Container(
                      decoration: BoxDecoration(
                        borderRadius: BorderRadius.circular(5.0),
                        color: Colors.deepPurple,
                      ),
                      height: 80,
                      width: 80,
                      child: Icon(
                        Icons.monetization_on,
                        color: Colors.white,
                        size: 40.0,
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 24.0,
                  ),
                  Center(
                    child: Text(
                      'Welcome back.',
                      style: TextStyle(
                        color: Colors.black54,
                        fontSize: 32.0,
                      ),
                    ),
                  ),
                  SizedBox(
                    height: 24.0,
                  ),
                  Padding(
                    padding: EdgeInsets.all(16.0),
                    child: TextFormField(
                      controller: _emailController,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(),
                        labelText: 'Email',
                      ),
                      keyboardType: TextInputType.emailAddress,
                    ),
                  ),
                  Padding(
                    padding: EdgeInsets.all(16.0),
                    child: TextFormField(
                      controller: _passwordController,
                      decoration: InputDecoration(
                        border: OutlineInputBorder(),
                        labelText: 'Password',
                      ),
                      obscureText: true,
                    ),
                  ),
                  Padding(
                    padding: EdgeInsets.all(16.0),
                    child: Container(
                      width: MediaQuery.of(context).size.width,
                      child: RaisedButton(
                        padding: EdgeInsets.all(24.0),
                        onPressed: () {
                          debugPrint('page');
                          final String email = _emailController.text;
                          final String password = _passwordController.text;
                          BlocProvider.of<SignInWithEmailBloc>(context).add(
                            SubmitSignInWithEmailEvent(email, password),
                          );
                        },
                        child: Text(
                          'Sign In',
                          style: TextStyle(
                            fontSize: 20.0,
                          ),
                        ),
                      ),
                    ),
                  ),
                ],
              ),
            ),
          );
        },
      ),
    );
  }
}
