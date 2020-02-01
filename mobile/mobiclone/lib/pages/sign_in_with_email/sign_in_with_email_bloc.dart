import 'package:bloc/bloc.dart';
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_event.dart';
import 'package:mobiclone/pages/sign_in_with_email/sign_in_with_email_state.dart';
import 'package:mobiclone/services/session_service.dart';

class SignInWithEmailBloc
    extends Bloc<SignInWithEmailEvent, SignInWithEmailState> {
  SessionService _sessionService;

  SignInWithEmailBloc(this._sessionService);

  @override
  SignInWithEmailState get initialState => InitiatedSignInWithEmailState();

  @override
  Stream<SignInWithEmailState> mapEventToState(
    SignInWithEmailEvent event,
  ) async* {
    yield SubmittedSignInWithEmailState();

    if (event is SubmitSignInWithEmailEvent) {
      if (event.email.isEmpty) {
        yield EmailRequiredSignInWithEmailState(
          event.email,
          'The email field must be informed.',
        );
      } else if (event.password.isEmpty) {
        yield PasswordRequiredSignInWithEmailState(
          event.password,
          'The password field must be informed.',
        );
      } else {
        final String token = await _sessionService.signWithEmailAndPassword(
          event.email,
          event.password,
        );

        if (token == null) {
          yield UnauthorizedSignInWithEmailState('Wrong email or password.');
        } else {
          yield AuthorizedSignInWithEmailState();
        }
      }
    }
  }
}
